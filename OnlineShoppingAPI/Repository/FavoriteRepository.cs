using Microsoft.EntityFrameworkCore;
using OnlineShoppingAPI.Entities;
using System.Security.Cryptography.Xml;


namespace OnlineShoppingAPI.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly OnlineShoppingContext _context;
       
        public FavoriteRepository(OnlineShoppingContext context)
        {
            _context = context;
            
        }
        public async Task Add(Favorite favorite)
        {
            try
            {
                await _context.Favorites.AddAsync(favorite);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var favorite = await _context.Favorites.FindAsync(id);
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<List<Favorite>> GetAllFavorites()
        {
            try
            {
                return await _context.Favorites.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Favorite> GetFavoriteById(Guid favoriteId)
        {
            try
            {
                var favorite = await _context.Favorites.FindAsync(favoriteId);
                return favorite;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Favorite favorite)
        {
            try
            {

                _context.Favorites.Update(favorite);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
