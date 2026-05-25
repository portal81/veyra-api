using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VeyraApi.Interfaces;
using VeyraApi.Models;
using VeyraApi.Data;

namespace VeyraApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;
    public AuthService(AppDbContext db, IConfiguration config) { _db = db; _config = config; }

    public async Task<string?> AuthenticateAsync(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;
        if (!VerifyPassword(password, user.PasswordHash)) return null;
        return GenerateToken(user.Id, user.Email, user.Role);
    }

    public string GenerateToken(string userId, string email, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT_SECRET"] ?? "veyra-default-secret-key-32-chars!!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "veyra-api",
            audience: "veyra-client",
            claims: new[] { new Claim(ClaimTypes.NameIdentifier, userId), new Claim(ClaimTypes.Email, email), new Claim(ClaimTypes.Role, role) },
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static bool VerifyPassword(string password, string hash)
    {
        var parts = hash.Split(':');
        if (parts.Length != 2) return false;
        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        return Convert.FromBase64String(parts[1]).SequenceEqual(pbkdf2.GetBytes(32));
    }
}
