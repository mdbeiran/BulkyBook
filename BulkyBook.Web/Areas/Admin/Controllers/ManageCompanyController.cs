using BulkyBook.Business.StaticTools;
using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.User;
using BulkyBook.Services.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class ManageCompanyController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public ManageCompanyController(IUnitOfWork unitOfWork)
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

        #region Upsert

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            Company company = new Company();

            if (id == null)
            {
                //Create
                return View(company);
            }

            //Update
            company = await _unitOfWork.CompanyRepository.
                        GetCompanyById(id.Value);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    //Create
                    await _unitOfWork.CompanyRepository.Insert(company);
                }
                else
                {
                    //Update
                    _unitOfWork.CompanyRepository.Update(company);
                }
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        #endregion

        #region Api Call

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            IEnumerable<Company> companies = await _unitOfWork.
                CompanyRepository.GetCompanies();
            return Json(new { data = companies });
        }


        [HttpDelete]
        public async Task Delete(int id)
        {
            Company company = await _unitOfWork.CompanyRepository.
                                GetCompanyById(id);

            if (company == null)
            {
                TempData["Error"] = "Error while deleting";
                //return Json(new { success = false, message = "Error while Deleting" });
            }

            await _unitOfWork.CompanyRepository.Delete(company);
            await _unitOfWork.Save();
            TempData["Success"] = "Delete Successful";

            //return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
