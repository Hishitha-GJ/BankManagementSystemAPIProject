using BankManagementSystemAPI.Models;

public class Bank
{
    public int BankId { get; set; }
    public string? BankName { get; set; } 
    public string? Branch { get; set; } 
    public string? Address { get; set; } 

    public ICollection<Customer>? Customers { get; set; }
}