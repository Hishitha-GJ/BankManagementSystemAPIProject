namespace BankManagementSystemAPI.DTOs
{
    public class BankDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
