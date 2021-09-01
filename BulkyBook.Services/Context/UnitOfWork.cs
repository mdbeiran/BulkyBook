using BulkyBook.DataAccess.Data;
using BulkyBook.Services.Repositories;
using BulkyBook.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Context
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Ctor

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(context);
            CoverTypeRepository = new CoverTypeRepository(context);
            BookRepository = new BookRepository(context);
        }

        #endregion

        public ICategoryRepository CategoryRepository { get; private set; }

        public ICoverTypeRepository CoverTypeRepository { get; private set; }

        public IBookRepository BookRepository { get; private set; }

        #region Save

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
             _context.Dispose();
        }

        #endregion
    }
}
