using Microsoft.AspNetCore.Mvc;
using VeyraApi.Interfaces;

namespace VeyraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HazemController : ControllerBase
{
    private readonly IHazemService _hazem;
    public HazemController(IHazemService hazem) => _hazem = hazem;

    [HttpPost("chat")]
    public async Task<ActionResult> Chat([FromBody] ChatRequest request)
    {
        if (string.IsNullOrEmpty(request.Message))
            return BadRequest(new { error = "Message is required" });
        var reply = await _hazem.ChatAsync(request.Message, request.SessionId ?? Guid.NewGuid().ToString());
        return Ok(new { reply });
    }
}

public record ChatRequest(string Message, string? SessionId);
