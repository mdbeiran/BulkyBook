using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Public;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories.Public
{
    public interface ISliderRepository
    {
        Task Insert(Slider slider);
        void Update(Slider slider);
        Task Delete(Slider slider);
        Task Delete(int sliderId);
        Task<IEnumerable<Slider>> GetSliders();
        Task<Slider> GetSliderById(int sliderId);
        Task<IEnumerable<Slider>> GetActiveSliders();
    }
}
