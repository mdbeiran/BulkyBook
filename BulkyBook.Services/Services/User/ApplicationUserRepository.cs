using BulkyBook.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BulkyBook.DomainClass.User;
using System.Threading.Tasks;
using BulkyBook.Services.Repositories.User;

namespace BulkyBook.Services.Services.User
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion
        
        #region Methods

        public async Task Insert(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string userId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _context.ApplicationUsers.Include(u => u.Company).ToListAsync();
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _context.ApplicationUsers.
                Include(u => u.Company).FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _context.ApplicationUsers.
                FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<int> GetCountUserRegistration()
        {
            return await _context.ApplicationUsers.CountAsync();
        }

        #endregion
    }
}
