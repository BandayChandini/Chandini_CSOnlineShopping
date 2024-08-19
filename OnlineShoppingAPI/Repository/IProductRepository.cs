using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task<Product> GetProductById(Guid productId);
        Task<List<Product>> GetAllProducts();
        Task Delete(Guid id);
        Task Update(Product product);

    }
}
