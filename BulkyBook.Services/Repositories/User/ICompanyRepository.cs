using BulkyBook.DomainClass.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories.User
{
    public interface ICompanyRepository
    {
        Task Insert(Company company);
        void Update(Company company);
        Task Delete(Company company);
        Task Delete(int company);
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompanyById(int companyId);
    }
}
