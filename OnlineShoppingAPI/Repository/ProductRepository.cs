using Microsoft.EntityFrameworkCore;
using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShoppingContext _context;
        

        public ProductRepository(OnlineShoppingContext context)
        {
            _context = context;
        }
        public async Task Add(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Delete(Guid productid)
        {
            try
            {
                var product = await _context.Products.FindAsync(productid);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
