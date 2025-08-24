using BankManagementSystemAPI.Models;

namespace BankManagementSystemAPI.DTOs
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; }
        public int AccountId { get; set; }
    }
}
