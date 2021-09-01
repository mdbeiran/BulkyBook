using BulkyBook.DomainClass.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories
{
    public interface IBookRepository
    {
        Task Insert(Book book);
        void Update(Book book);
        Task Delete(Book book);
        Task Delete(int bookId);
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int bookId);
        Task<IEnumerable<Book>> LastBooks();
    }
}
