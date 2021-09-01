using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.Book;
using BulkyBook.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BulkyBook.Services.Services
{
    public class BookRepository : IBookRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public void Update(Book book)
        {
            Book mainBook = _context.Books.Find(book.Id);

            if (mainBook != null)
            {
                if (book.ImageUrl != null)
                {
                    mainBook.ImageUrl = book.ImageUrl;
                }

                mainBook.Title = book.Title;
                mainBook.Description = book.Description;
                mainBook.Author = book.Author;
                mainBook.ListPrice = book.ListPrice;
                mainBook.Price = book.Price;
                mainBook.Price50 = book.Price50;
                mainBook.Price100 = book.Price100;
                mainBook.ISBN = book.ISBN;
                mainBook.CategoryId = book.CategoryId;
                mainBook.CategoryId = book.CategoryId;
            }
        }

        public async Task Delete(Book book)
        {
            _context.Books.Remove(book);
        }

        public async Task Delete(int bookId)
        {
            Book book = await _context.Books.FindAsync(bookId);
            await Delete(book);
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.Include(b => b.Category).
                    Include(b => b.CoverType).ToListAsync();
        }

        public async Task<Book> GetBookById(int bookId)
        {
            return await _context.Books.Include(b => b.Category).
                    Include(b => b.CoverType).
                    FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<IEnumerable<Book>> LastBooks()
        {
            return await _context.Books.Include(b => b.Category)
                .Include(b => b.CoverType).
                OrderByDescending(b => b.Id).Take(6).ToListAsync();
        }

        #endregion
    }
}
