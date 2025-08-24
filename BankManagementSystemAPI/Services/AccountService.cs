using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using BankManagementSystemAPI.Repositories;

namespace BankManagementSystemAPI.Services
{
    public class AccountService : IAccount
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _accountRepository.GetAccountByIdAsync(id);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            return await _accountRepository.CreateAccountAsync(account);
        }

        public async Task<Account?> UpdateAccountAsync(int id, Account account)
        {
            return await _accountRepository.UpdateAccountAsync(id, account);
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            return await _accountRepository.DeleteAccountAsync(id);
        }

        public async Task<decimal> GetBalanceAsync(int accountId)
        {
            return await _accountRepository.GetBalanceAsync(accountId);
        }

        public async Task<bool> DepositAsync(int accountId, decimal amount)
        {
            return await _accountRepository.DepositAsync(accountId, amount);
        }

        public async Task<bool> WithdrawAsync(int accountId, decimal amount)
        {
            return await _accountRepository.WithdrawAsync(accountId, amount);
        }

        public async Task<bool> TransferAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            return await _accountRepository.TransferAsync(fromAccountId, toAccountId, amount);
        }

        public async Task<IEnumerable<Account>> SearchAccountsAsync(string? accountNumber, int? customerId)
        {
            return await _accountRepository.SearchAccountsAsync(accountNumber,customerId);
        }
        public async Task<int> CountAccountsAsync(string? accountNumber, int? customerId)
        {
            return await _accountRepository.CountAccountsAsync(accountNumber, customerId);
        }
    }
}
