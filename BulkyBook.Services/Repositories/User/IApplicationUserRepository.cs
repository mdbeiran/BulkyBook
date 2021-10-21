using BulkyBook.DomainClass.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories.User
{
    public interface IApplicationUserRepository
    {
        Task Insert(ApplicationUser user);
        void Update(ApplicationUser user);
        Task Delete(ApplicationUser user);
        Task Delete(string userId);
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUserById(string userId);
        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
