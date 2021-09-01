using BulkyBook.DomainClass.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories
{
    public interface ICategoryRepository
    {
        Task Insert(Category category);
        void Update(Category category);
        Task Delete(Category category);
        Task Delete(int categoryId);
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int categoryId);
    }
}
