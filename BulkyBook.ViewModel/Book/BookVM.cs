using System;
using System.Collections.Generic;
using System.Text;
using BulkyBook.DomainClass.Book;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.ViewModel.Book
{
    public class BookVM
    {
        public BulkyBook.DomainClass.Book.Book Book { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
    }
}
