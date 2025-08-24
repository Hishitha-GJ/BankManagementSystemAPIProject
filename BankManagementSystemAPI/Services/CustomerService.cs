using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using BankManagementSystemAPI.Repositories;

namespace BankManagementSystemAPI.Services
{
    public class CustomerService : ICustomer
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // --- CRUD ---
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<Customer?> GetCustomerWithAccountsAsync(int id)
        {
            return await _customerRepository.GetCustomerWithAccountsAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            return await _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task<Customer?> UpdateCustomerAsync(int id, Customer customer)
        {
            return await _customerRepository.UpdateCustomerAsync(id, customer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _customerRepository.DeleteCustomerAsync(id);
        }

        // --- Search, Filter, Count ---
        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string? name = null, string? email = null)
        {
            return await _customerRepository.SearchCustomersAsync(name, email);
        }

        public async Task<int> CountCustomersAsync()
        {
            return await _customerRepository.CountCustomersAsync();
        }
    }
}
