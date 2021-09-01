using BulkyBook.DomainClass.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories
{
    public interface ICoverTypeRepository
    {
        Task Insert(CoverType coverType);
        void Update(CoverType coverType);
        Task Delete(CoverType coverType);
        Task Delete(int coverTypeId);
        Task<IEnumerable<CoverType>> GetCoverTypes();
        Task<CoverType> GetCoverTypeById(int coverTypeId);
    }
}
