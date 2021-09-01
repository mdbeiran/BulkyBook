using BulkyBook.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Context
{
    public interface IUnitOfWork : IDisposable
    {
         ICategoryRepository CategoryRepository { get; }
         ICoverTypeRepository CoverTypeRepository { get; }
         IBookRepository BookRepository { get; }

        #region Save

        Task Save();

        #endregion
    }
}
