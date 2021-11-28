using BulkyBook.Business.StaticTools;
using BulkyBook.Business.User;
using BulkyBook.DomainClass.Public;
using BulkyBook.DomainClass.Setting;
using BulkyBook.Services.Context;
using BulkyBook.ViewModel.Public;
using BulkyBook.ViewModel.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

        #region About Us

        [HttpGet]
        [Route("AboutUs")]
        public async Task<IActionResult> AboutUs()
        {
            var siteSetting = await _unitOfWork.
                SiteSettingRepository.GetSiteSetting();

            return View(siteSetting);
        }

        #endregion

        #region Contact Us

        [HttpGet]
        [Route("ContactUs")]
        public async Task<IActionResult> ContactUs()
        {
            SettingAndContactVM settingAndContact = new SettingAndContactVM()
            {
                SiteSetting = await _unitOfWork.SiteSettingRepository.GetSiteSetting(),
                ContactUs = new ContactUs()
            };

            return View(settingAndContact);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ContactUs")]
        public async Task<IActionResult> ContactUs(ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                contactUs.CreateDate = DateTime.Now;
                if (User.Identity.IsAuthenticated)
                {
                    #region Get the logged in user ID

                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                    #endregion
                    contactUs.UserId = claim.Value;
                }

                contactUs.UserIp = UserManager.GetUserIp();
                await _unitOfWork.ContactUsRepository.Insert(contactUs);
                await _unitOfWork.Save();

                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            return View(contactUs);
        }

        #endregion

        #region Site Rules

        [HttpGet]
        [Route("SiteRules")]
        public async Task<IActionResult> SiteRules()
        {
            var siteSetting = await _unitOfWork.
                SiteSettingRepository.GetSiteSetting();

            return View(siteSetting);
        }

        #endregion

        #region Privacy

        [Route("Privacy")]
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
