using BulkyBook.Business.StaticTools;
using BulkyBook.Services.Context;
using BulkyBook.ViewModel.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class HomeController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            NumberOfUsersAndNewOrders number = new NumberOfUsersAndNewOrders()
            {
                NumberOfUsersRegistration = await _unitOfWork.ApplicationUserRepository.GetCountUserRegistration(),
                NumberOfNewOrders = await _unitOfWork.OrderHeaderRepository.
                    GetCountNewOrders()
            };

            return View(number);
        }

        #endregion
    }
}
