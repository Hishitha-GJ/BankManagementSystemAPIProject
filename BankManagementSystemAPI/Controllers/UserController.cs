using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        // --- CRUD ---
        // Get all users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers() =>
            Ok(await _userService.GetAllUsersAsync());

        // Get user by id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // Get user by username
        [HttpGet("username/{username}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // Create new user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var created = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = created.Id }, created);
        }

        // Update user by id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var updated = await _userService.UpdateUserAsync(id, user);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // Delete user by id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!await _userService.DeleteUserAsync(id)) return NotFound();
            return NoContent();
        }

        // --- Search & Filter ---

        /// Example: /api/user/search?role=Admin
        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchUsers([FromQuery] string? role)
        {
            var result = await _userService.SearchUsersAsync(role);
            return Ok(result);
        }

        // --- Count ---
        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountUsers()
        {
            var count = await _userService.CountUsersAsync();
            return Ok(new { TotalUsers = count });
        }
    }
}
