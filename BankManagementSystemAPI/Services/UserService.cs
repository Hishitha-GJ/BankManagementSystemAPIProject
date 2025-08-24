using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using BankManagementSystemAPI.Repositories;

namespace BankManagementSystemAPI.Services
{
    public class UserService : IUser
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // --- CRUD ---
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            return await _userRepository.UpdateUserAsync(id, user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        // --- Search, Filter, Count ---
       
        public async Task<IEnumerable<User>> SearchUsersAsync( string? role = null)
        {
            return await _userRepository.SearchUsersAsync( role);
        }

        public async Task<int> CountUsersAsync()
        {
            return await _userRepository.CountUsersAsync();
        }
    }
}
