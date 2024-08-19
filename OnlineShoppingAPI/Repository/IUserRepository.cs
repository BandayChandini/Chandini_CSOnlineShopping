using OnlineShoppingAPI.Entities;
namespace OnlineShoppingAPI.Repository
{
    public interface IUserRepository
    {
        Task Register(User user);
        Task<User> ValidUser(string email, string password);
        Task<List<User>> GetAllUsers();
        Task DeleteUser(string UserId);
        Task UpdateUser(User user);
    }
}
