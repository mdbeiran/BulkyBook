using BulkyBook.Business.StaticTools;
using BulkyBook.DomainClass.Ticketing;
using BulkyBook.Services.Context;
using BulkyBook.ViewModel.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ManageTicketController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public ManageTicketController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            #region Get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion

            var currentUser = await _unitOfWork.ApplicationUserRepository.GetUserById(claim.Value);
            TicketVM ticketVM = new TicketVM()
            {
                FullName = currentUser.FullName
            };

            return View(ticketVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketVM ticketVM)
        {
            if (ModelState.IsValid)
            {
                #region Get id logged in user

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                #endregion

                var currentUser = await _unitOfWork.ApplicationUserRepository.GetUserById(claim.Value);

                Ticket ticket = new Ticket()
                {
                    ApplicationUserId = claim.Value,
                    Subject = ticketVM.Subject,
                    Status = true
                };
                await _unitOfWork.TicketRepository.Insert(ticket);
                await _unitOfWork.Save();

                TicketMessage ticketMessage = new TicketMessage()
                {
                    TicketId = ticket.Id,
                    ApplicationUserId = claim.Value,
                    Description = ticketVM.Description
                };
                await _unitOfWork.TicketMessageRepository.Insert(ticketMessage);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

            }

            return View(ticketVM);
        }

        #endregion

        #region View Ticket

        //id = ticketId
        [HttpGet]
        public async Task<IActionResult> TicketDetail(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            #region Get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion

            var currentUser = await _unitOfWork.ApplicationUserRepository.GetUserById(claim.Value);
            var ticket = await _unitOfWork.TicketRepository.GetTicketById(id);

            TicketMessageVM ticketMessageVM = new TicketMessageVM()
            {
                TicketMessageWithRoleVMs = await _unitOfWork.TicketMessageRepository.GetTicketMessagesByTicketId(id),
                TicketMessage = new TicketMessage()
                {
                    TicketId = id,
                    Ticket = ticket
                },
                FullName = currentUser.FullName,
                Subject = ticket.Subject
            };

            return View(ticketMessageVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TicketDetail(TicketMessage ticketMessage)
        {
            #region Get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion
            var currentUser = await _unitOfWork.ApplicationUserRepository.GetUserById(claim.Value);

            if (ModelState.IsValid)
            {
                TicketMessage message = new TicketMessage()
                {
                    TicketId = ticketMessage.TicketId,
                    ApplicationUserId = currentUser.Id,
                    Description = ticketMessage.Description
                };

                await _unitOfWork.TicketMessageRepository.Insert(message);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            var ticket = await _unitOfWork.TicketRepository.GetTicketById(ticketMessage.TicketId);
            TicketMessageVM ticketMessageVM = new TicketMessageVM()
            {
                TicketMessageWithRoleVMs = await _unitOfWork.TicketMessageRepository.GetTicketMessagesByTicketId(ticketMessage.TicketId),
                TicketMessage = new TicketMessage(),
                FullName = currentUser.FullName,
                Email = currentUser.Email,
                Subject = ticket.Subject
            };

            return View(ticketMessage);

        }

        #endregion

        #region Api Call

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            #region Get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion

            IEnumerable<Ticket> tickets;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                // Retrieve All Ticket
                tickets = await _unitOfWork.TicketRepository.GetTickets();
            }
            else
            {
                // Retrieve All Ticket Related to Current user
                tickets = await _unitOfWork.TicketRepository.GetTicketsByUserId(claim.Value);
            }

            return Json(new { data = tickets });
        }


        // id = TicketId
        [HttpPost]
        public async Task<IActionResult> OpenCloseTicket(int id)
        {
            Ticket ticket = await _unitOfWork.TicketRepository.GetTicketById(id);

            if (ticket == null)
            {
                return Json(new { success = false, message = "Error while Open/Close Ticket" });
            }

            if (ticket.Status)
            {
                // The ticket must be closed
                ticket.Status = false;
            }
            else
            {
                // The ticket must be opened
                ticket.Status = true;
            }

             _unitOfWork.TicketRepository.Update(ticket);
            await _unitOfWork.Save();

            return Json(new { success = true, message = "Operation Successful" });
        }

        #endregion
    }
}
