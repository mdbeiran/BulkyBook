using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.Book;
using BulkyBook.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Services
{
    public class CoverTypeRepository : ICoverTypeRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public CoverTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(CoverType coverType)
        {
            await _context.CoverTypes.AddAsync(coverType);
        }

        public void Update(CoverType coverType)
        {
            _context.CoverTypes.Update(coverType);
        }

        public async Task Delete(CoverType coverType)
        {
              _context.CoverTypes.Remove(coverType);
        }

        public async Task Delete(int coverTypeId)
        {
            CoverType coverType = await _context.CoverTypes.FindAsync(coverTypeId);
            await Delete(coverType);
        }

        public async Task<IEnumerable<CoverType>> GetCoverTypes()
        {
            return  await _context.CoverTypes.ToListAsync();
        }

        public async Task<CoverType> GetCoverTypeById(int coverTypeId)
        {
            return await _context.CoverTypes.FindAsync(coverTypeId);
        }

        #endregion
    }
}
