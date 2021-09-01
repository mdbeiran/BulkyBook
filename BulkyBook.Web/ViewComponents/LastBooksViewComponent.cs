using BulkyBook.DomainClass.Book;
using BulkyBook.Services.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Web.ViewComponents
{
    public class LastBooksViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public LastBooksViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Book> books = await _unitOfWork.
                BookRepository.GetBooks();

            return View(books);
        }
    }
}
