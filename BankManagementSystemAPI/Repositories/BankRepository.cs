using BankManagementSystemAPI.Data;
using BankManagementSystemAPI.Models;
using BankManagementSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankManagementSystemAPI.Repositories
{
    public class BankRepository : IBank
    {
        private readonly AppDbContext _context;

        public BankRepository(AppDbContext context)
        {
            _context = context;
        }

        // --- CRUD ---
        public async Task<IEnumerable<Bank>> GetAllBanksAsync()
        {
            return await _context.Banks.Include(b => b.Customers).ToListAsync();
        }

        public async Task<Bank?> GetBankByIdAsync(int id)
        {
            return await _context.Banks.Include(b => b.Customers).FirstOrDefaultAsync(b => b.BankId == id);
        }

        public async Task<Bank> CreateBankAsync(Bank bank)
        {
            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();
            return bank;
        }

        public async Task<Bank?> UpdateBankAsync(int id, Bank bank)
        {
            var existing = await _context.Banks.FindAsync(id);
            if (existing == null) return null;

            existing.BankName = bank.BankName;
            existing.Branch = bank.Branch;
            existing.Address = bank.Address;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteBankAsync(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null) return false;

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();
            return true;
        }

        

    
    }
}
