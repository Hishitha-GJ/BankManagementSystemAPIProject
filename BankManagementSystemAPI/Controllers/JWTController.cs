using BankManagementSystemAPI.Data;
using BankManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BankManagementSystemAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public JWTController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ✅ Login - Generate JWT Token
        [HttpPost("login")]
        public IActionResult GenerateToken([FromBody] LoginDTO model)
        {
            // 1. Validate user from DB
            var user = _context.Users.SingleOrDefault(u =>
                u.Username == model.Username &&
                u.Password == model.Password &&
                u.Role == model.Role);

            if (user == null)
            {
                return Unauthorized("Invalid username, password, or role");
            }

            // 2. Claims (include role)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role) // Admin / User
            };

            // 3. Signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Token
            var expireMinutes = Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            // 5. Return token
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                role = user.Role
            });
        }


    }
}