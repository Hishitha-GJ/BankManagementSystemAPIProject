using BankManagementSystemAPI.Data;
using BankManagementSystemAPI.Models;
using BankManagementSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankManagementSystemAPI.Repositories
{
    public class TransactionRepository : ITransaction
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        // --- CRUD ---
        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .ThenInclude(a => a.Customer)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .ThenInclude(a => a.Customer)
                .FirstOrDefaultAsync(t => t.TransactionId == id);
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .Include(t => t.Account)
                .ToListAsync();
        }

        // --- Search, Filter, Count ---
        public async Task<IEnumerable<Transaction>> SearchTransactionsAsync(
            int? accountId = null,
            TransactionType? type = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _context.Transactions.Include(t => t.Account).AsQueryable();

            if (accountId.HasValue)
                query = query.Where(t => t.AccountId == accountId.Value);

            if (type.HasValue)
                query = query.Where(t => t.Type == type.Value);

            if (from.HasValue)
                query = query.Where(t => t.TransactionDate >= from.Value);

            if (to.HasValue)
                query = query.Where(t => t.TransactionDate <= to.Value);

            return await query.ToListAsync();
        }

        public async Task<int> CountTransactionsAsync()
        {
            return await _context.Transactions.CountAsync();
        }
        public async Task<int> CountTransactionsByAccountIdAsync(int accountId)
        {
            return await _context.Transactions.CountAsync(t => t.AccountId == accountId);
        }
    }
}
