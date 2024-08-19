using Microsoft.EntityFrameworkCore;
using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OnlineShoppingContext _context;
       

        public OrderItemRepository(OnlineShoppingContext context)
        {
            _context = context;
        }
        public async Task<List<OrderItem>> GetAllOrderItems()
        {
            try
            {
                return await _context.OrderItems.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<OrderItem> GetOrderItemById(Guid id)
        {
            try
            {
                var items = await _context.OrderItems.FindAsync(id);
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddOrderItem(OrderItem orderitem)
        {
            try
            {
                await _context.OrderItems.AddAsync(orderitem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
