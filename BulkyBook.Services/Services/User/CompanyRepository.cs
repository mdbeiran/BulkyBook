using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.User;
using BulkyBook.Services.Repositories.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Services.User
{
    public class CompanyRepository : ICompanyRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public void Update(Company company)
        {
            _context.Companies.Update(company);
        }

        public async Task Delete(Company company)
        {
            _context.Companies.Remove(company);
        }

        public async Task Delete(int companyId)
        {
            Company company = await _context.Companies.
                FindAsync(companyId);
            await Delete(company);
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyById(int companyId)
        {
            return await _context.Companies.FindAsync(companyId);
        }

        #endregion
    }
}
