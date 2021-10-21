using BulkyBook.Business.StaticTools;
using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Order;
using BulkyBook.Services.Context;
using BulkyBook.Utility.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public BookController(IUnitOfWork unitOfWork)
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

        #region Details

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Book book = await _unitOfWork.BookRepository.
                GetBookById(id);
            ShoppingCart cart = new ShoppingCart()
            {
                Book = book,
                BookId = book.Id
            };

            return View(cart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(ShoppingCart cartObject)
        {
            cartObject.Id = 0;

            if (ModelState.IsValid)
            {
                #region Get UserId

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                #endregion

                cartObject.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = await _unitOfWork.ShoppingCartRepository.
                    GetCartByUserIdAndBookId(
                    cartObject.ApplicationUserId, cartObject.BookId);
                if (cartFromDb == null)
                {
                    // no record exist in database for that product for that user
                    await _unitOfWork.ShoppingCartRepository.Insert(cartObject);
                }
                else
                {
                    cartFromDb.Count += cartObject.Count;
                     //_unitOfWork.ShoppingCartRepository.Update(cartFromDb);
                }

                await _unitOfWork.Save();

                #region Add item to session

                int count = await _unitOfWork.ShoppingCartRepository
                    .GetCountShoppingCartByUserId(
                    cartObject.ApplicationUserId);

                HttpContext.Session.SetInt32(SD.ssShoppingCart, count);
                //HttpContext.Session.SetObject(SD.ssShoppingCart, cartObject);
                //HttpContext.Session.GetObject<ShoppingCart>(SD.ssShoppingCart);

                #endregion

                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                Book book = await _unitOfWork.BookRepository.
                GetBookById(cartObject.BookId);

                ShoppingCart cart = new ShoppingCart()
                {
                    Book = book,
                    BookId = book.Id
                };

                return View(cart);
            }
            
        }

        #endregion
    }
}
