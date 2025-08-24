using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _customerService;

        public CustomerController(ICustomer customerService)
        {
            _customerService = customerService;
        }

        // --- CRUD ---
        //api/customer
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCustomers() =>
            Ok(await _customerService.GetAllCustomersAsync());

        //api/customer/1
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        //api/customer/1/accounts
        [HttpGet("{id}/accounts")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetCustomerWithAccounts(int id)
        {
            var customer = await _customerService.GetCustomerWithAccountsAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        //api/customer
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            var created = await _customerService.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = created.CustomerId }, created);
        }

        //api/customer/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            var updated = await _customerService.UpdateCustomerAsync(id, customer);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        //api/customer/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!await _customerService.DeleteCustomerAsync(id)) return NotFound();
            return NoContent();
        }

        // --- Search & Filter ---
        [HttpGet("search")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> SearchCustomers([FromQuery] string? name, [FromQuery] string? email)
        {
            var result = await _customerService.SearchCustomersAsync(name, email);
            return Ok(result);
        }

        // --- Count ---
        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountCustomers()
        {
            var count = await _customerService.CountCustomersAsync();
            return Ok(new { TotalCustomers = count });
        }
    }
}
