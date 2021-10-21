using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Public;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.ViewModel.Book
{
    public class CategoryVM
    {
        public IEnumerable<Category> Categories { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
