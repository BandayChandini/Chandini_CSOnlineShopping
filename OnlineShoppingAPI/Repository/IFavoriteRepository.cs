using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public interface IFavoriteRepository
    {
        Task Add(Favorite favorite);
        Task<Favorite> GetFavoriteById(Guid favoriteId);
        Task<List<Favorite>> GetAllFavorites();
        Task Delete(Guid id);
        Task Update(Favorite favorite);
    }
}
