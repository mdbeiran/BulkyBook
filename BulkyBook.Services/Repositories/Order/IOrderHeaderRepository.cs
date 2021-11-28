using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories
{
    public interface IOrderHeaderRepository
    {
        Task Insert(OrderHeader orderHeader);
        void Update(OrderHeader orderHeader);
        Task Delete(OrderHeader orderHeader);
        Task Delete(int orderHeaderId);
        Task<IEnumerable<OrderHeader>> GetOrderHeaders();
        Task<OrderHeader> GetOrderHeaderById(int orderHeaderId);
        Task<IEnumerable<OrderHeader>> GetOrderHeadersByUserId(string userId);
        Task<int> GetCountNewOrders();
    }
}
