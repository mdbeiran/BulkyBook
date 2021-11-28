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
    public class SiteSettingRepository : ISiteSettingRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public SiteSettingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task<SiteSetting> GetSiteSetting()
        {
            return await _context.SiteSettings.FirstOrDefaultAsync();
        }

        public async Task Insert(SiteSetting siteSetting)
        {
            await _context.SiteSettings.AddAsync(siteSetting);
        }

        public async Task Update(SiteSetting siteSetting)
        {
            var local = _context.Set<SiteSetting>()
              .Local
              .FirstOrDefault(f => f.SettingId == siteSetting.SettingId);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(siteSetting).State = EntityState.Modified;
        }

        #endregion
    }
}
