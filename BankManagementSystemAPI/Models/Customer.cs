using BankManagementSystemAPI.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; } 
    public string? Email { get; set; }
    public string? Phone { get; set; } 
    public int BankId { get; set; }
    public Bank? Bank { get; set; }

    public ICollection<Account>? Accounts { get; set; } 
    public User? User { get; set; }
}
