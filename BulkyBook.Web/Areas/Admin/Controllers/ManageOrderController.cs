using BulkyBook.Business.StaticTools;
using BulkyBook.DomainClass.Order;
using BulkyBook.Services.Context;
using BulkyBook.ViewModel.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ManageOrderController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public ManageOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        [BindProperty]
        public OrderDetailsVM OrderDetailsVM { get; set; }

        #region Index

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Order Details

        [HttpGet]
        public async Task<IActionResult> OrderDetails(int id)
        {
            OrderDetailsVM orderDetailsVM = new OrderDetailsVM()
            {
                OrderHeader = await _unitOfWork.OrderHeaderRepository.
                GetOrderHeaderById(id),
                OrderDetails = await _unitOfWork.OrderDetailsRepository.
                GetOrderDetailsByOrderId(id)
            };

            return View(orderDetailsVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDetails(string stripeToken)
        {
            OrderHeader orderHeader = await _unitOfWork.OrderHeaderRepository.
                GetOrderHeaderById(OrderDetailsVM.OrderHeader.Id);

            if (stripeToken != null)
            {
                // process the payment
                ChargeCreateOptions options = new ChargeCreateOptions()
                {
                    Amount = Convert.ToInt32(orderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Description = "Order Id : " + orderHeader.Id,
                    Source = stripeToken
                };

                ChargeService service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (charge.Id == null)
                {
                    orderHeader.PaymentStatus = SD.PaymentStatusRejected;
                }
                else
                {
                    orderHeader.TransactionId = charge.Id;
                }

                if (charge.Status.ToLower() == "succeeded")
                {
                    orderHeader.PaymentStatus = SD.PaymentStatusApproved;
                    orderHeader.PaymentDate = DateTime.Now;
                }

                await _unitOfWork.Save();
            }

            return RedirectToAction(nameof(OrderDetails), "ManageOrder", new { id = orderHeader.Id });
        }

        #endregion

        #region Start Processing

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public async Task<IActionResult> StartProcessing(int id)
        {
            OrderHeader orderHeader = await _unitOfWork.OrderHeaderRepository.
                GetOrderHeaderById(id);

            orderHeader.OrderStatus = SD.StatusInProcess;

            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index), "ManageOrder");
        }

        #endregion

        #region Ship Order

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public async Task<IActionResult> ShipOrder()
        {
            OrderHeader orderHeader = await _unitOfWork.OrderHeaderRepository.
                GetOrderHeaderById(OrderDetailsVM.OrderHeader.Id);

            orderHeader.Carrier = OrderDetailsVM.OrderHeader.Carrier;
            orderHeader.TrackingNumber = OrderDetailsVM.OrderHeader.TrackingNumber;
            orderHeader.OrderStatus = SD.StatusShipped;
            orderHeader.ShippingDate = DateTime.Now;

            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Cancel Order

        [HttpGet]
        public async Task<IActionResult> CancelOrder(int id)
        {
            OrderHeader orderHeader = await _unitOfWork.OrderHeaderRepository.
                GetOrderHeaderById(id);

            if (orderHeader.PaymentStatus == SD.PaymentStatusApproved)
            {
                // مشتری پول پرداخت کرده
                RefundCreateOptions options = new RefundCreateOptions()
                {
                    Amount = Convert.ToInt32(orderHeader.OrderTotal * 100),
                    Reason = RefundReasons.RequestedByCustomer,
                    Charge = orderHeader.TransactionId
                };

                RefundService service = new RefundService();
                Refund refund = await service.CreateAsync(options);

                orderHeader.OrderStatus = SD.StatusRefunded;
                orderHeader.PaymentStatus = SD.StatusRefunded;
            }
            else
            {
                // مشتری پول پرداخت نکرده
                orderHeader.OrderStatus = SD.StatusCancelled;
                orderHeader.PaymentStatus = SD.StatusCancelled;
            }

            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Api Call

        [HttpGet]
        public async Task<IActionResult> GetOrders(string status)
        {
            #region Get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion

            IEnumerable<OrderHeader> orders;

            if (User.IsInRole(SD.Role_Admin) || (User.IsInRole(SD.Role_Employee)))
            {
                // Retrieve all orders
                orders = await _unitOfWork.OrderHeaderRepository.
                    GetOrderHeaders();
            }
            else
            {
                // Retreive all orders related to user
                orders = await _unitOfWork.OrderHeaderRepository.
                    GetOrderHeadersByUserId(claim.Value);
            }

            switch (status)
            {
                case "inProcess":
                    orders = orders.Where(
                     o => o.OrderStatus == SD.StatusPending ||
                     o.OrderStatus == SD.StatusApproved ||
                     o.OrderStatus == SD.StatusInProcess);
                    break;

                case "pending":
                    orders = orders.Where(
                        o => o.PaymentStatus == SD.PaymentStatusDelayedPayment);
                    break;

                case "completed":
                    orders = orders.Where(
                        o => o.OrderStatus == SD.StatusShipped);
                    break;

                case "rejected":
                    orders = orders.Where(
                        o => o.OrderStatus == SD.StatusRefunded ||
                        o.OrderStatus == SD.StatusCancelled ||
                        o.OrderStatus == SD.PaymentStatusRejected);
                    break;

                default:
                    break;
            }

            return Json(new { data = orders });
        }

        #endregion
    }
}
