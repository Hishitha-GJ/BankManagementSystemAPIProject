using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using BankManagementSystemAPI.Repositories;

namespace BankManagementSystemAPI.Services
{
    public class BankService : IBank
    {
        private readonly BankRepository _bankRepository;

        public BankService(BankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<IEnumerable<Bank>> GetAllBanksAsync()
        {
            return await _bankRepository.GetAllBanksAsync();
        }

        public async Task<Bank?> GetBankByIdAsync(int id)
        {
            return await _bankRepository.GetBankByIdAsync(id);
        }

        public async Task<Bank> CreateBankAsync(Bank bank)
        {
            return await _bankRepository.CreateBankAsync(bank);
        }

        public async Task<Bank?> UpdateBankAsync(int id, Bank bank)
        {
            return await _bankRepository.UpdateBankAsync(id, bank);
        }

        public async Task<bool> DeleteBankAsync(int id)
        {
            return await _bankRepository.DeleteBankAsync(id);
        }

   
    }
}
