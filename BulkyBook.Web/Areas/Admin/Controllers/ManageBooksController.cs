using BulkyBook.Business.StaticTools;
using BulkyBook.DomainClass.Book;
using BulkyBook.Services.Context;
using BulkyBook.ViewModel.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ManageBooksController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ManageBooksController(IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
            IEnumerable<Category> categories = await _unitOfWork.
                CategoryRepository.GetCategories();
            IEnumerable<CoverType> coverTypes = await _unitOfWork.
                CoverTypeRepository.GetCoverTypes();

            BookVM bookVM = new BookVM()
            {
                Book = new Book(),
                CategoryList = categories.Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = coverTypes.Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                // Create
                return View(bookVM);
            }

            // Update
            bookVM.Book = await _unitOfWork.BookRepository.
                GetBookById(id.Value);

            if (bookVM.Book == null)
            {
                return NotFound();
            }

            return View(bookVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(BookVM bookVM)
        {
            IEnumerable<Category> categories = await _unitOfWork.
                CategoryRepository.GetCategories();
            IEnumerable<CoverType> coverTypes = await _unitOfWork.
                CoverTypeRepository.GetCoverTypes();

            if (ModelState.IsValid)
            {
                // img
                string webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var uploadsOrigin = Path.Combine(webRootPath, @"images/Books/Origin");

                    #region Delete old image When Edit Book

                    // this is an edit and we need to remove old image
                    if (bookVM.Book.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath,
                            bookVM.Book.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    #endregion

                    #region Stor original image on Server

                    using (var filesStreams = new FileStream(Path.Combine(uploadsOrigin,
                        fileName + extension), FileMode.Create))
                    {
                        await files[0].CopyToAsync(filesStreams);
                    }

                    #endregion

                    bookVM.Book.ImageUrl = @"\images\Books\Origin\" + fileName + extension;
                }
                else
                {
                    // Update when they do not change the image
                    if (bookVM.Book.Id != 0)
                    {
                        Book mainBook = await _unitOfWork.BookRepository.
                            GetBookById(bookVM.Book.Id);
                        bookVM.Book.ImageUrl = mainBook.ImageUrl;
                    }
                }

                // Add Book And Edit Book
                if (bookVM.Book.Id == 0)
                {
                    await _unitOfWork.BookRepository.Insert(bookVM.Book);
                }
                else
                {
                    _unitOfWork.BookRepository.Update(bookVM.Book);
                }

                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                bookVM.CategoryList = categories.Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                });
                bookVM.CoverTypeList = coverTypes.Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                });

                if (bookVM.Book.Id != 0)
                {
                    // Edit Mode
                    bookVM.Book = await _unitOfWork.BookRepository.
                        GetBookById(bookVM.Book.Id);
                }
            }

            return View(bookVM);
        }

        #endregion

        #region Api Call

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            IEnumerable<Book> books = await _unitOfWork.BookRepository
                                        .GetBooks();
            return Json(new { data = books });
        }


        [HttpDelete]
        public async Task Delete(int id)
        {
            Book book = await _unitOfWork.BookRepository
                .GetBookById(id);
            if (book == null)
            {
                TempData["Error"] = "Error while Deleting";
            }

            // Delete image from server
            string webRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(webRootPath, book.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            // Delete Book
            await _unitOfWork.BookRepository.Delete(book);
            await _unitOfWork.Save();
            TempData["Success"] = "Delete Successful";
        }

        #endregion
    }
}
