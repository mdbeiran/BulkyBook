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
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public OrderDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(OrderDetails orderDetails)
        {
            await _context.OrderDetails.AddAsync(orderDetails);
        }

        public void Update(OrderDetails orderDetails)
        {
            _context.OrderDetails.Update(orderDetails);
        }

        public async Task Delete(OrderDetails orderDetails)
        {
              _context.OrderDetails.Remove(orderDetails);
        }

        public async Task Delete(int orderDetailsId)
        {
            OrderDetails orderDetails = await _context.
                OrderDetails.FindAsync(orderDetailsId);
            await Delete(orderDetails);
        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetails()
        {
            return  await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetails> GetOrderDetailsById(int orderDetailsId)
        {
            return await _context.OrderDetails.FindAsync(orderDetailsId);
        }

        

        #endregion
    }
}
