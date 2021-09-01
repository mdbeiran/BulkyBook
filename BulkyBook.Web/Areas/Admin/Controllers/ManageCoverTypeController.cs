using BulkyBook.DomainClass.Book;
using BulkyBook.Services.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageCoverTypeController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public ManageCoverTypeController(IUnitOfWork unitOfWork)
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
            CoverType coverType = new CoverType();

            if (id == null)
            {
                //Create
                return View(coverType);
            }

            //Update
            coverType = await _unitOfWork.CoverTypeRepository.
                        GetCoverTypeById(id.Value);
            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                if (coverType.Id == 0)
                {
                    //Create
                    await _unitOfWork.CoverTypeRepository.Insert(coverType);
                }
                else
                {
                    //Update
                    _unitOfWork.CoverTypeRepository.Update(coverType);
                }
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(coverType);
        }

        #endregion

        #region Api Call

        [HttpGet]
        public async Task<IActionResult> GetCoverTypes()
        {
            IEnumerable<CoverType> coverTypes = await _unitOfWork.CoverTypeRepository.
                                                GetCoverTypes();
            return Json(new { data = coverTypes });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            CoverType coverType = await _unitOfWork.CoverTypeRepository.
                                GetCoverTypeById(id);

            if (coverType == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            await _unitOfWork.CoverTypeRepository.Delete(coverType);
            await _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
