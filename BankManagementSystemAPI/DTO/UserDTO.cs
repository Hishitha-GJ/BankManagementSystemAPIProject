namespace BankManagementSystemAPI.DTOs
{
    // Response DTO (to send role info back to client, safe to expose)
    public class UserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    // Request DTO (login credentials only)
    public class LoginDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;


    }
}
