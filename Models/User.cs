namespace VeyraApi.Models;

public class AppUser
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "admin";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
