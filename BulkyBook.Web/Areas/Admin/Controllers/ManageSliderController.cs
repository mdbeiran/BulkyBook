using BulkyBook.Business.StaticTools;
using BulkyBook.DomainClass.Public;
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
    [Authorize(Roles = SD.Role_Admin)]
    public class ManageSliderController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public ManageSliderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {

            return View(); ;
        }

        #endregion

        #region Upsert

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            Slider slider = new Slider();

            if (id == null)
            {
                //Create
                return View(slider);
            }

            //Update
            slider = await _unitOfWork.SliderRepository.
                        GetSliderById(id.Value);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Slider slider)
        {
            if (ModelState.IsValid)
            {
                if (slider.Id == 0)
                {
                    //Create
                    await _unitOfWork.SliderRepository.Insert(slider);
                }
                else
                {
                    //Update
                    _unitOfWork.SliderRepository.Update(slider);
                }
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }

        #endregion

        #region Api Call

        [HttpGet]
        public async Task<IActionResult> GetAllSliders()
        {
            IEnumerable<Slider> sliders = await _unitOfWork.
                SliderRepository.GetSliders();

            return Json(new { data = sliders });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await _unitOfWork.SliderRepository.
                                GetSliderById(id);

            if (slider == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            await _unitOfWork.SliderRepository.Delete(slider);
            await _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
