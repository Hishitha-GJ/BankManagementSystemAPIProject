using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using BankManagementSystemAPI.Repositories;

namespace BankManagementSystemAPI.Services
{
    public class TransactionService : ITransaction
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionService(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // --- CRUD ---
        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetTransactionByIdAsync(id);
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            return await _transactionRepository.CreateTransactionAsync(transaction);
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            return await _transactionRepository.DeleteTransactionAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
        }

        // --- Search, Filter, Count ---
        public async Task<IEnumerable<Transaction>> SearchTransactionsAsync(
            int? accountId = null,
            TransactionType? type = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            return await _transactionRepository.SearchTransactionsAsync(accountId, type, from, to);
        }

        public async Task<int> CountTransactionsAsync()
        {
            return await _transactionRepository.CountTransactionsAsync();
        }
        public async Task<int> CountTransactionsByAccountIdAsync(int accountId)
        {
            return await _transactionRepository.CountTransactionsByAccountIdAsync(accountId);
        }
    }
}
