using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Order;
using BulkyBook.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartById(int shoppingCartId)
        {
            return await _context.ShoppingCarts.Include(u => u.ApplicationUser).
                Include(b => b.Book).FirstOrDefaultAsync(c => c.Id == shoppingCartId);
        }

        public async Task<ShoppingCart> GetCartByUserIdAndBookId(string userId, int bookId)
        {
            return await _context.ShoppingCarts.Include(b => b.Book).
                 FirstOrDefaultAsync(u => u.ApplicationUserId == userId && u.BookId == bookId);
        }

        public async Task<int> GetCountShoppingCartByUserId(string userId)
        {
            var carts = await _context.ShoppingCarts.
                Where(c => c.ApplicationUserId == userId).ToListAsync();

            return carts.Count();
        }

        public async Task<IEnumerable<ShoppingCart>> GetCartsByUserId(string userId)
        {
            return await _context.ShoppingCarts.
                Include(u => u.ApplicationUser).Include(b => b.Book)
                .Where(c => c.ApplicationUserId == userId).ToListAsync();
        }

        public async Task DeleteCarts(IEnumerable<ShoppingCart> carts)
        {
             _context.ShoppingCarts.RemoveRange(carts);
        }



        #endregion
    }
}
