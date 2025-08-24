namespace BankManagementSystemAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
        public string? Role { get; set; }  // Admin or User

        public int? CustomerId { get; set; } // null for Admin
        public Customer? Customer { get; set; }
    }
}
