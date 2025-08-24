using BankManagementSystemAPI.Models;

namespace BankManagementSystemAPI.Interfaces
{
    public interface IAccount
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int id);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account?> UpdateAccountAsync(int id, Account account);
        Task<bool> DeleteAccountAsync(int id);

        // Banking operations
        Task<decimal> GetBalanceAsync(int accountId);
        Task<bool> DepositAsync(int accountId, decimal amount);
        Task<bool> WithdrawAsync(int accountId, decimal amount);
        Task<bool> TransferAsync(int fromAccountId, int toAccountId, decimal amount);

        // Extra: search, filter, count
        Task<IEnumerable<Account>> SearchAccountsAsync(string? accountNumber, int? customerId);
        Task<int> CountAccountsAsync(string? accountNumber = null, int? customerId = null);



    }
}
