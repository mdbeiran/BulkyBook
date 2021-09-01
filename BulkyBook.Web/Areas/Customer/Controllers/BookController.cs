using BulkyBook.DomainClass.Book;
using BulkyBook.Services.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Details(int? id)
        {
            return View();
        }

        #endregion
    }
}
