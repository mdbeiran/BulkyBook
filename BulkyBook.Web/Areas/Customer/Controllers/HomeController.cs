using BulkyBook.Business.StaticTools;
using BulkyBook.Services.Context;
using BulkyBook.ViewModel.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        #region Ctor

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            #region Get the logged in user ID

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion

            if (claim != null)
            {
                // User Logged in
                int count = await _unitOfWork.
                    ShoppingCartRepository.
                    GetCountShoppingCartByUserId(claim.Value);
                HttpContext.Session.SetInt32(SD.ssShoppingCart, count);
            }

            return View();
        }

        #endregion

        #region Slider

        public async Task<IActionResult> ShowSlider()
        {
            return View();
        }

        #endregion

        #region Privacy

        public IActionResult Privacy()
        {
            return View();
        }

        #endregion

        #region Error

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
