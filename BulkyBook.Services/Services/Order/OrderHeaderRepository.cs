using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Order;
using BulkyBook.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Services
{
    public class OrderHeaderRepository : IOrderHeaderRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(OrderHeader orderHeader)
        {
            await _context.OrderHeaders.AddAsync(orderHeader);
        }

        public void Update(OrderHeader orderHeader)
        {
            _context.OrderHeaders.Update(orderHeader);
        }

        public async Task Delete(OrderHeader orderHeader)
        {
              _context.OrderHeaders.Remove(orderHeader);
        }

        public async Task Delete(int orderHeaderId)
        {
            OrderHeader orderHeader = await _context.
                OrderHeaders.FindAsync(orderHeaderId);
            await Delete(orderHeader);
        }

        public async Task<IEnumerable<OrderHeader>> GetOrderHeaders()
        {
            return  await _context.OrderHeaders.ToListAsync();
        }

        public async Task<OrderHeader> GetOrderHeaderById(int orderHeaderId)
        {
            return await _context.OrderHeaders.FindAsync(orderHeaderId);
        }

        

        #endregion
    }
}
