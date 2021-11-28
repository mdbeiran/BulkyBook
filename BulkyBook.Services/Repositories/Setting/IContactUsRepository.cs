using BulkyBook.DomainClass.Setting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories.Setting
{
    public interface IContactUsRepository
    {
        Task Insert(ContactUs contactUs);
        Task Update(ContactUs contactUs);
        Task<IEnumerable<ContactUs>> GetAllContactUs();
        Task<ContactUs> GetContactUsById(int contactUsId);
        Task<IEnumerable<ContactUs>> GetNewestContactUs();

    }
}
