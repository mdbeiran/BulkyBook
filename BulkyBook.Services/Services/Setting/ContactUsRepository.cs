using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.Setting;
using BulkyBook.Services.Repositories.Setting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Services.Setting
{
    public class ContactUsRepository : IContactUsRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public ContactUsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(ContactUs contactUs)
        {
            await _context.ContactUs.AddAsync(contactUs);
        }

        public async Task Update(ContactUs contactUs)
        {
            _context.ContactUs.Update(contactUs);
        }

        public async Task<IEnumerable<ContactUs>> GetAllContactUs()
        {
            return await _context.ContactUs.ToListAsync();
        }

        public async Task<ContactUs> GetContactUsById(int contactUsId)
        {
            return await _context.ContactUs.FindAsync(contactUsId);
        }

        public async Task<IEnumerable<ContactUs>> GetNewestContactUs()
        {
            return await _context.ContactUs.OrderByDescending(c => c.CreateDate).
                Where(c => c.IsRead == false).Take(6).ToListAsync();
        }

        #endregion

    }
}
