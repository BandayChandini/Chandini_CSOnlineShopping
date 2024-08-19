using Microsoft.EntityFrameworkCore;
using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        
        private readonly OnlineShoppingContext _context;
       
     
        public UserRepository(OnlineShoppingContext context)
        {
            _context = context;
            
        }
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task Register(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task DeleteUser(string UserId)
        {
            try
            {
                var user = await _context.Users.FindAsync(UserId);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<User> ValidUser(string email, string password)
        {
            try
            {
                
                    var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email & u.Password == password);
                    if (user != null)
                    {
                        return user;
                    }
                    else
                    {
                        Console.WriteLine("User validation failed.");
                        return null;
                    }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("an Error Ocucured" + ex.Message);
                return null;

            }

        }
    }
}
