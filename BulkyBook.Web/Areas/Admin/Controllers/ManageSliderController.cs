using BulkyBook.Business.StaticTools;
using BulkyBook.DomainClass.Public;
using BulkyBook.Services.Context;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ManageSliderController(IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
                // image
                string webRootPath = _webHostEnvironment.WebRootPath;
                var fileUpload = HttpContext.Request.Form.Files;

                if (fileUpload.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(fileUpload[0].FileName);

                    #region Delete old image when edit 

                    if (slider.ImageName != null)
                    {
                        // This is an edit and we need to remove old image
                        if (System.IO.File.Exists(SD.OriginalSliderImagePath +
                            slider.ImageName))
                        {
                            System.IO.File.Delete(SD.OriginalSliderImagePath + slider.ImageName);
                        }
                        if (System.IO.File.Exists(SD.ThumbSliderImagePath +
                            slider.ImageName))
                        {
                            System.IO.File.Delete(SD.ThumbSliderImagePath + slider.ImageName);
                        }
                    }

                    #endregion

                    #region Store original image on server

                    using (var filesStreams = new FileStream(SD.OriginalSliderImagePath +
                        fileName + extension, FileMode.Create))
                    {
                        await fileUpload[0].CopyToAsync(filesStreams);
                    }

                    #endregion

                    #region Image Resize for thumbnail

                    var img = Image.FromFile(SD.OriginalSliderImagePath +
                        fileName + extension);
                    var scaleImage = ImageResize.Scale(img, 200, 200);
                    scaleImage.SaveAs(SD.ThumbSliderImagePath + fileName + extension);

                    #endregion

                    slider.ImageName = fileName + extension;
                }

                #region Add Or Edit Slider

                if (slider.Id == 0)
                {
                    //Create
                    slider.Visit = 0;
                    await _unitOfWork.SliderRepository.Insert(slider);
                }
                else
                {
                    //Update
                    _unitOfWork.SliderRepository.Update(slider);
                }

                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

                #endregion
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

            #region Delete img from server

            if (System.IO.File.Exists(SD.OriginalSliderImagePath +
                            slider.ImageName))
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(webRootPath,
                           SD.OriginalSliderImagePath.TrimStart('\\') ,slider.ImageName);
                System.IO.File.Delete(imagePath);

                //System.IO.File.Delete(
                //    SD.OriginalSliderImagePath + slider.ImageName);
            }
            if (System.IO.File.Exists(
                SD.ThumbSliderImagePath + slider.ImageName))
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(webRootPath,
                           SD.ThumbSliderImagePath.TrimStart('\\') , slider.ImageName);
                System.IO.File.Delete(imagePath);

                //System.IO.File.Delete(
                //    SD.ThumbSliderImagePath + slider.ImageName);
            }

            #endregion

            // Delete slider object
            await _unitOfWork.SliderRepository.Delete(slider.Id);
            await _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
