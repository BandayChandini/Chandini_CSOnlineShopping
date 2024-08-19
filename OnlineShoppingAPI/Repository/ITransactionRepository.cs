using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetTransactionByDate(DateTime date);
        Task<List<Transaction>> GetTransactionById(string id);
        
        Task AddTransaction(Transaction transaction);
    }
}
