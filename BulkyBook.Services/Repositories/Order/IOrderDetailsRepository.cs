using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories
{
    public interface IOrderDetailsRepository
    {
        Task Insert(OrderDetails orderDetails);
        void Update(OrderDetails orderDetails);
        Task Delete(OrderDetails orderDetails);
        Task Delete(int orderDetailsId);
        Task<IEnumerable<OrderDetails>> GetOrderDetails();
        Task<OrderDetails> GetOrderDetailsById(int orderDetailsId);
        Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderId(int orderId);
    }
}
