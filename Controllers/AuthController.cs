using Microsoft.AspNetCore.Mvc;
using VeyraApi.Interfaces;

namespace VeyraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _auth.AuthenticateAsync(request.Email, request.Password);
        if (token == null) return Unauthorized(new { error = "Invalid credentials" });
        return Ok(new { token });
    }
}

public record LoginRequest(string Email, string Password);
