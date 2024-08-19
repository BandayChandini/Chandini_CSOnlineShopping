using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly OnlineShoppingContext _context;
        

        public CartItemRepository(OnlineShoppingContext context)
        {
           _context = context;
            
        }

        public async Task AddCartItem(CartItem cartitem)
        {
            try
            {
                await _context.CartItems.AddAsync(cartitem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteCartItem(Guid id)
        {
            try
            {
                var item = await _context.CartItems.FindAsync(id);
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<List<CartItem>> GetAllCartItems()
        {
            try
            {
                return await _context.CartItems.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CartItem> GetCartItemById(Guid id)
        {
            try
            {
                var item = await _context.CartItems.FindAsync(id);
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCartItem(CartItem cartitem)
        {
            try
            {
                _context.CartItems.Update(cartitem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
