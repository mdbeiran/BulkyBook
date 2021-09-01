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
    public class CategoryRepository : ICategoryRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task Delete(Category category)
        {
              _context.Categories.Remove(category);
        }

        public async Task Delete(int categoryId)
        {
            Category category = await _context.Categories.FindAsync(categoryId);
            await Delete(category);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return  await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        

        #endregion
    }
}
