using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories
{
    public interface IShoppingCartRepository
    {
        Task Insert(ShoppingCart shoppingCart);
        void Update(ShoppingCart shoppingCart);
        Task Delete(ShoppingCart shoppingCart);
        Task Delete(int shoppingCartId);
        Task<IEnumerable<ShoppingCart>> GetShoppingCarts();
        Task<ShoppingCart> GetShoppingCartById(int shoppingCartId);
        Task<ShoppingCart> GetCartByUserIdAndBookId(string userId, int bookId);
        Task<int> GetCountShoppingCartByUserId(string userId);
        Task<IEnumerable<ShoppingCart>> GetCartsByUserId(string userId);
        Task DeleteCarts(IEnumerable<ShoppingCart> carts);
    }
}
