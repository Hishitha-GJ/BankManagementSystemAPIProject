using BankManagementSystemAPI.Data;
using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagementSystemAPI.Repositories
{
    public class UserRepository : IUser
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // --- CRUD ---
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Customer).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Customer).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) return null;

            existing.Username = user.Username;
            existing.Password = user.Password;
            existing.Role = user.Role;
            existing.CustomerId = user.CustomerId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // --- Search, Filter, Count ---

        public async Task<IEnumerable<User>> SearchUsersAsync(
           
            string? role = null)
        {
            var query = _context.Users.Include(u => u.Customer).AsQueryable();
                if (!string.IsNullOrEmpty(role))
                query = query.Where(u => u.Role == role);

            return await query.ToListAsync();
        }

        public async Task<int> CountUsersAsync()
        {
            return await _context.Users.CountAsync();
        }
    }
}
