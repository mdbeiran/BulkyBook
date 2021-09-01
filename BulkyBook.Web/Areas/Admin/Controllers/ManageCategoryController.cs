using BulkyBook.DataAccess.Data;
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
    public class ManageCategoryController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public ManageCategoryController(IUnitOfWork unitOfWork)
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
            Category category = new Category();

            if (id == null)
            {
                //Create
                return View(category);
            }

            //Update
            category = await _unitOfWork.CategoryRepository.
                        GetCategoryById(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    //Create
                    await _unitOfWork.CategoryRepository.Insert(category);
                }
                else
                {
                    //Update
                    _unitOfWork.CategoryRepository.Update(category);
                }
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #endregion

        #region Api Call

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            IEnumerable<Category> categories = await _unitOfWork.CategoryRepository.
                                                GetCategories();
            return Json(new { data = categories });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _unitOfWork.CategoryRepository.
                                GetCategoryById(id);

            if (category == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            await _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
