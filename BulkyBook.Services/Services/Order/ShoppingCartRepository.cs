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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(ShoppingCart shoppingCart)
        {
            await _context.ShoppingCarts.AddAsync(shoppingCart);
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
        }

        public async Task Delete(ShoppingCart shoppingCart)
        {
              _context.ShoppingCarts.Remove(shoppingCart);
        }

        public async Task Delete(int shoppingCartId)
        {
            ShoppingCart shoppingCart = await _context.
                ShoppingCarts.FindAsync(shoppingCartId);
            await Delete(shoppingCart);
        }

        public async Task<IEnumerable<ShoppingCart>> GetShoppingCarts()
        {
            return  await _context.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartById(int shoppingCartId)
        {
            return await _context.ShoppingCarts.FindAsync(shoppingCartId);
        }

        

        #endregion
    }
}
