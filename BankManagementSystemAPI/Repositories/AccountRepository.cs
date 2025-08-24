using BankManagementSystemAPI.Data;
using BankManagementSystemAPI.Models;
using BankManagementSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankManagementSystemAPI.Repositories
{
    public class AccountRepository : IAccount
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        // --- CRUD ---
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts
                .Include(a => a.Customer)
                .ThenInclude(c => c.Bank)
                .Include(a => a.Transactions)
                .ToListAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts
                .Include(a => a.Customer)
                .ThenInclude(c => c.Bank)
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> UpdateAccountAsync(int id, Account account)
        {
            var existing = await _context.Accounts.FindAsync(id);
            if (existing == null) return null;

            existing.AccountNumber = account.AccountNumber;
            existing.Balance = account.Balance;
            existing.CustomerId = account.CustomerId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        // --- Banking Operations ---
        public async Task<decimal> GetBalanceAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null) throw new Exception("Account not found");
            return account.Balance;
        }

        public async Task<bool> DepositAsync(int accountId, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null) return false;

            account.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Deposit
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> WithdrawAsync(int accountId, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null || account.Balance < amount) return false;

            account.Balance -= amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Withdrawal
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TransferAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            if (fromAccountId == toAccountId) return false;

            var fromAccount = await _context.Accounts.FindAsync(fromAccountId);
            var toAccount = await _context.Accounts.FindAsync(toAccountId);

            if (fromAccount == null || toAccount == null) return false;
            if (fromAccount.Balance < amount) return false;

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = fromAccountId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Transfer
            });

            _context.Transactions.Add(new Transaction
            {
                AccountId = toAccountId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Deposit
            });

            await _context.SaveChangesAsync();
            return true;
        }

        // --- Search, Filter, Count ---
        public async Task<IEnumerable<Account>> SearchAccountsAsync(
         string? accountNumber = null,
         int? customerId = null)
        {
            var query = _context.Accounts
                .Include(a => a.Customer)
                 .ThenInclude(c => c.User)      // Include User inside Customer
                .Include(a => a.Transactions)     // Include Transactions
                .AsQueryable();

            if (!string.IsNullOrEmpty(accountNumber))
                query = query.Where(a => a.AccountNumber.Contains(accountNumber));

            if (customerId.HasValue)
                query = query.Where(a => a.CustomerId == customerId.Value);

            return await query.ToListAsync();
        }

        public async Task<int> CountAccountsAsync(string? accountNumber = null, int? customerId = null)
        {
            var query = _context.Accounts.AsQueryable();

            if (!string.IsNullOrEmpty(accountNumber))
                query = query.Where(a => a.AccountNumber.Contains(accountNumber));

            if (customerId.HasValue)
                query = query.Where(a => a.CustomerId == customerId.Value);

            return await query.CountAsync();
        }


    }
}
