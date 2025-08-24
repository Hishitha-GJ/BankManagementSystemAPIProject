using BankManagementSystemAPI.DTOs;
using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountService;

        public AccountController(IAccount accountService)
        {
            _accountService = accountService;
        }

        // --- CRUD ---
        //GET api/account   
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAccounts() =>
            Ok(await _accountService.GetAllAccountsAsync());

        //GET api/account/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        //POST api/account
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            var created = await _accountService.CreateAccountAsync(account);
            return CreatedAtAction(nameof(GetAccountById), new { id = created.AccountId }, created);
        }

        //PUT api/account/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] Account account)
        {
            var updated = await _accountService.UpdateAccountAsync(id, account);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        //DELETE api/account/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (!await _accountService.DeleteAccountAsync(id)) return NotFound();
            return NoContent();
        }

        // --- Banking operations ---

        [HttpGet("{id}/balance")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetBalance(int id)
        {
            var balance = await _accountService.GetBalanceAsync(id);
            return Ok(new { AccountId = id, Balance = balance });
        }

        [HttpPost("{id}/deposit")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Deposit(int id, [FromQuery] decimal amount)
        {
            var success = await _accountService.DepositAsync(id, amount);
            return success ? Ok($"Deposited {amount} to account {id}") : BadRequest("Deposit failed");
        }

        [HttpPost("{id}/withdraw")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Withdraw(int id, [FromQuery] decimal amount)
        {
            var success = await _accountService.WithdrawAsync(id, amount);
            return success ? Ok($"Withdrawn {amount} from account {id}") : BadRequest("Withdrawal failed");
        }

        [HttpPost("transfer")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO transfer)
        {
            var success = await _accountService.TransferAsync(transfer.FromAccountId, transfer.ToAccountId, transfer.Amount);
            return success ? Ok("Transfer successful") : BadRequest("Transfer failed");
        }

        // --- Search, Filter, Count ---
        [HttpGet("search")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> SearchAccounts(
      [FromQuery] string? accountNumber,
      [FromQuery] int? customerId)
        {
            var result = await _accountService.SearchAccountsAsync(accountNumber, customerId);

            if (result == null || !result.Any())
                return NotFound("No accounts found matching the criteria.");

            return Ok(result);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountAccounts()
        {
            var count = await _accountService.CountAccountsAsync();
            return Ok(new { TotalAccounts = count });
        }
    }
}
