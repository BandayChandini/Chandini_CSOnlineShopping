using OnlineShoppingAPI.Entities;
namespace OnlineShoppingAPI.Repository
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task<Order> GetOrderById(Guid orderId);
        Task<List<Order>> GetAllOrders();
        Task Delete(Guid id);
        
    }
}
