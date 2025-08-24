namespace BankManagementSystemAPI.DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
    }

    public class TransferDTO
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
