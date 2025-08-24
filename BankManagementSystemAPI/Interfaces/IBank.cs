using BankManagementSystemAPI.Models;

namespace BankManagementSystemAPI.Interfaces
{
    public interface IBank
    {
        Task<IEnumerable<Bank>> GetAllBanksAsync();
        Task<Bank?> GetBankByIdAsync(int id);
        Task<Bank> CreateBankAsync(Bank bank);
        Task<Bank?> UpdateBankAsync(int id, Bank bank);
        Task<bool> DeleteBankAsync(int id);

     
    }
}
