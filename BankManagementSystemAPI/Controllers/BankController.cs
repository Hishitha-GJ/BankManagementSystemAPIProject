using BankManagementSystemAPI.Interfaces;
using BankManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBank _bankService;

        public BankController(IBank bankService)
        {
            _bankService = bankService;
        }

        // --- CRUD ---
        //API GET
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllBanks() =>
            Ok(await _bankService.GetAllBanksAsync());

        //API GET by id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetBankById(int id)
        {
            var bank = await _bankService.GetBankByIdAsync(id);
            if (bank == null) return NotFound();
            return Ok(bank);
        }

        //API CREATE BANK
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateBank([FromBody] Bank bank)
        {
            var created = await _bankService.CreateBankAsync(bank);
            return CreatedAtAction(nameof(GetBankById), new { id = created.BankId }, created);
        }

        //API UPDATE BANK
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBank(int id, [FromBody] Bank bank)
        {
            var updated = await _bankService.UpdateBankAsync(id, bank);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        //API DELETE BANK
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            if (!await _bankService.DeleteBankAsync(id)) return NotFound();
            return NoContent();
        }

       
    }
}
