using BulkyBook.DomainClass.Setting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories.Setting
{
    public interface ISiteSettingRepository
    {
        Task Insert(SiteSetting siteSetting);
        Task Update(SiteSetting siteSetting);
        Task<SiteSetting> GetSiteSetting();
    }
}
