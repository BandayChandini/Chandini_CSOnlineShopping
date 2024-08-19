using Microsoft.EntityFrameworkCore;
using OnlineShoppingAPI.Entities;

namespace OnlineShoppingAPI.Repository
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly OnlineShoppingContext _context;
        

        public TransactionRepository(OnlineShoppingContext context)
        {
            _context = context;
        }
        public async Task AddTransaction(Transaction transaction)
        {
            try
            {
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<Transaction> GetTransactionByDate(DateTime date)
        {
            try
            {
               var transaction= await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionDate.Date == date.Date);
                
                return transaction;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Transaction>> GetTransactionById(string id)
        {
            try
            {
                var transactions = await _context.Transactions
                    .Where(t => t.UserId == id)
                    .ToListAsync();
                return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
