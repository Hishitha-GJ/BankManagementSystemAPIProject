using BankManagementSystemAPI.Models;

namespace BankManagementSystemAPI.Interfaces
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);

        // Extra: search, filter, count
        Task<IEnumerable<User>> SearchUsersAsync(string? role = null);
        Task<int> CountUsersAsync();
    }
}
