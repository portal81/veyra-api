namespace VeyraApi.Interfaces;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(string email, string password);
    string GenerateToken(string userId, string email, string role);
}
