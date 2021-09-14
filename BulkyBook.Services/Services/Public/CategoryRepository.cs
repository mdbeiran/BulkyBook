using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Public;
using BulkyBook.Services.Repositories;
using BulkyBook.Services.Repositories.Public;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Services.Public
{
    public class SliderRepository : ISliderRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public SliderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(Slider slider)
        {
            await _context.Sliders.AddAsync(slider);
        }

        public void Update(Slider slider)
        {
            _context.Sliders.Update(slider);
        }

        public async Task Delete(Slider slider)
        {
              _context.Sliders.Remove(slider);
        }

        public async Task Delete(int sliderId)
        {
            Slider slider = await _context.Sliders.
                FindAsync(sliderId);
            await Delete(slider);
        }

        public async Task<IEnumerable<Slider>> GetSliders()
        {
            return  await _context.Sliders.ToListAsync();
        }

        public async Task<Slider> GetSliderById(int sliderId)
        {
            return await _context.Sliders.FindAsync(sliderId);
        }

        #endregion
    }
}
