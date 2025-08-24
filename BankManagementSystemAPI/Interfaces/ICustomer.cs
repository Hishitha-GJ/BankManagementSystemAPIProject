using BankManagementSystemAPI.Models;

namespace BankManagementSystemAPI.Interfaces
{
    public interface ICustomer
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer?> UpdateCustomerAsync(int id, Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<Customer?> GetCustomerWithAccountsAsync(int id);

        // Extra: search, filter, count
        Task<IEnumerable<Customer>> SearchCustomersAsync(string? name = null, string? email = null);
        Task<int> CountCustomersAsync();
    }
}
