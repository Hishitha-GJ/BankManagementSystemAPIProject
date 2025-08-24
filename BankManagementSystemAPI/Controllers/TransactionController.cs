using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _transactionService;

        public TransactionController(ITransaction transactionService)
        {
            _transactionService = transactionService;
        }

        // --- CRUD ---
        //api/transaction/GET
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAllTransactions() =>
            Ok(await _transactionService.GetAllTransactionsAsync());

        //api/transaction/1
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        //api/transaction
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            var created = await _transactionService.CreateTransactionAsync(transaction);
            return CreatedAtAction(nameof(GetTransactionById), new { id = created.TransactionId }, created);
        }

        //api/transaction/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteTransaction(int id)
        {
            if (!await _transactionService.DeleteTransactionAsync(id)) return NotFound();
            return NoContent();
        }

        // --- Get by Account ---
        [HttpGet("account/{accountId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetTransactionsByAccount(int accountId) =>
            Ok(await _transactionService.GetTransactionsByAccountIdAsync(accountId));


        // --- Search & Filter ---
        [HttpGet("search")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> SearchTransactions(
            [FromQuery] int? accountId,
            [FromQuery] TransactionType? type,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var result = await _transactionService.SearchTransactionsAsync(accountId, type, from, to);
            return Ok(result);
        }

        // --- Count ---
        // Count all transactions
        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountTransactions()
        {
            var count = await _transactionService.CountTransactionsAsync();
            return Ok(new { TotalTransactions = count });
        }

        // Count transactions by AccountId
        [HttpGet("count/account/{accountId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> CountTransactionsByAccount(int accountId)
        {
            var count = await _transactionService.CountTransactionsByAccountIdAsync(accountId);
            return Ok(new { AccountId = accountId, TotalTransactions = count });
        }
    }
}
