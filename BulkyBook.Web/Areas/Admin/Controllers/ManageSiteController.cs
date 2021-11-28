using BulkyBook.DomainClass.Setting;
using BulkyBook.Services.Context;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageSiteController : Controller
    {
        #region Ctoe

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        public ManageSiteController(IUnitOfWork unitOfWork,
            IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        #endregion

        #region Edit Site Setting

        [HttpGet]
        public async Task<IActionResult> EditSiteSetting()
        {
            var siteSetting = await _unitOfWork.SiteSettingRepository.GetSiteSetting();

            if (siteSetting == null)
            {
                SiteSetting setting = new SiteSetting()
                {
                    SettingId = 1,
                    SiteName = "Test",
                    SiteDescription = "test",
                    SiteEmail = "test@test.com",
                    SiteTell = "xxxxx",
                    SiteMobile = "xxxxxxxx",
                    SiteFax = "xxxxx",
                    Address = "test"
                };
                await _unitOfWork.SiteSettingRepository.Insert(setting);
                await _unitOfWork.Save();
                var siteSettingCreated = await _unitOfWork.SiteSettingRepository.GetSiteSetting();

                return View(siteSettingCreated);
            }
            else
            {
                return View(siteSetting);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSiteSetting(SiteSetting siteSetting)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.SiteSettingRepository.Update(siteSetting);
                await _unitOfWork.Save();

                return View("EditSiteSetting", siteSetting);
            }

            return View(siteSetting);
        }

        #endregion

        #region Contact Us

        #region Contact List

        public async Task<IActionResult> ContactList()
        {

            return View();
        }

        #endregion

        #region ShowMessage

        [HttpGet]
        public async Task<IActionResult> ShowMessage(int id)
        {
            var contactUs = await _unitOfWork.ContactUsRepository.
                GetContactUsById(id);

            contactUs.IsRead = true;
            await _unitOfWork.Save();

            return View(contactUs);
        }

        #endregion

        #region Answer To Contact

        [HttpGet]
        public async Task<IActionResult> AnswerContact(int id)
        {
            var contactUs = await _unitOfWork.ContactUsRepository.
                GetContactUsById(id);

            return View(contactUs);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnswerContact(ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(contactUs.Answer))
                {
                    await _unitOfWork.ContactUsRepository.Update(contactUs);
                    await _unitOfWork.Save();

                    #region Send Message to Contact Email

                    await _emailSender.SendEmailAsync(contactUs.Email, "Answer Your Questions",
                            $"<h1>From The BulkyBook Website</h2><hr />" +
                            $"<div>Your question : {contactUs.Message}</div><br />" +
                            $"<div>Your answer : {contactUs.Answer}</div>");

                    #endregion

                    return RedirectToAction("ContactList");
                }
            }

            return View(contactUs);
        }

        #endregion

        #endregion

        #region Api Call

        public async Task<IActionResult> GetAllContactUs()
        {
            var allContact = await _unitOfWork.ContactUsRepository.
                GetAllContactUs();

            foreach (var contact in allContact)
            {
                contact.CreateDate = Convert.ToDateTime(contact.CreateDate.ToShortDateString());
            }

            return Json(new { data = allContact });
        }

        #endregion
    }
}
