using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAllOrderItems();
        Task<OrderItem> GetOrderItemById(Guid id);
        Task AddOrderItem(OrderItem orderitem);

    }
}
