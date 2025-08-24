namespace BankManagementSystemAPI.Models
{

    public enum TransactionType { Deposit = 1, Withdrawal = 2, Transfer = 3 }

    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; }

        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
