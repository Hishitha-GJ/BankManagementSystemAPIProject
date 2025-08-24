using BankManagementSystemAPI.Models;

namespace BankManagementSystemAPI.Interfaces
{
    public interface ITransaction
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        Task<bool> DeleteTransactionAsync(int id);

        // Extra: search, filter, count
        Task<IEnumerable<Transaction>> SearchTransactionsAsync(int? accountId = null, TransactionType? type = null, DateTime? from = null, DateTime? to = null);
        Task<int> CountTransactionsAsync();
        Task<int> CountTransactionsByAccountIdAsync(int accountId);
    }
}
