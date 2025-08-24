namespace BankManagementSystemAPI.Interfaces
{
    public interface IJWTService
    {
        string GenerateToken(string username, string role);
    }
}
