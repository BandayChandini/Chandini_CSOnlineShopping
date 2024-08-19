using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetAllCartItems();
        Task<CartItem> GetCartItemById(Guid id);
        Task DeleteCartItem(Guid id);
        Task UpdateCartItem(CartItem cartitem);
        Task AddCartItem(CartItem cartitem);
    }
}
