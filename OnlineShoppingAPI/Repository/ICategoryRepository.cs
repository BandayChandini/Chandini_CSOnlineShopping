using OnlineShoppingAPI.Entities;
namespace OnlineShoppingAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(Guid id);
        Task DeleteCategory(Guid id);
        Task UpdateCategory(Category category);
        Task AddCategory(Category category);

    }
}
