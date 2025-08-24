namespace BankManagementSystemAPI.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int BankId { get; set; }
    }
}
