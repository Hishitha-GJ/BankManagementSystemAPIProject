using BankManagementSystemAPI.Data;
using BankManagementSystemAPI.Models;
using BankManagementSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankManagementSystemAPI.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        // --- CRUD ---
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.Include(c => c.Accounts).Include(c => c.Bank).ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Accounts)
                .Include(c => c.Bank)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer?> GetCustomerWithAccountsAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateCustomerAsync(int id, Customer customer)
        {
            var existing = await _context.Customers.FindAsync(id);
            if (existing == null) return null;

            existing.CustomerName = customer.CustomerName;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;
            existing.BankId = customer.BankId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        // --- Search, Filter, Count ---
        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string? name = null, string? email = null)
        {
            var query = _context.Customers.Include(c => c.Accounts).Include(c => c.Bank).AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.CustomerName.Contains(name));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Email.Contains(email));

            return await query.ToListAsync();
        }

        public async Task<int> CountCustomersAsync()
        {
            return await _context.Customers.CountAsync();
        }
    }
}
