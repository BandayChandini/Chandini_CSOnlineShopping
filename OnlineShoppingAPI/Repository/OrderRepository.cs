using Microsoft.EntityFrameworkCore;
using OnlineShoppingAPI.Entities;
using OnlineShoppingAPI.Repository;
namespace OnlineShoppingAPI.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly OnlineShoppingContext _context;
        

        public OrderRepository(OnlineShoppingContext context)
        {
            _context = context;
        }
        public async Task Add(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Delete(Guid orderid)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderid);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                return await _context.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> GetOrderById(Guid orderId)
        {
            try {
                var orders = await _context.Orders.FindAsync(orderId);
                return orders;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            } 
        }
    }
}
