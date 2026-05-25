using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeyraApi.Interfaces;
using VeyraApi.Models;

namespace VeyraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadsController : ControllerBase
{
    private readonly ILeadRepository _repo;
    public LeadsController(ILeadRepository repo) => _repo = repo;

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Lead>>> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Lead>> GetById(string id)
    {
        var lead = await _repo.GetByIdAsync(id);
        if (lead == null) return NotFound();
        return Ok(lead);
    }

    [HttpPost]
    public async Task<ActionResult<Lead>> Create([FromBody] Lead lead)
    {
        if (string.IsNullOrEmpty(lead.FullName) || string.IsNullOrEmpty(lead.Phone) || string.IsNullOrEmpty(lead.Service))
            return BadRequest(new { error = "FullName, Phone, and Service are required" });
        var created = await _repo.CreateAsync(lead);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<ActionResult<Lead>> Update(string id, [FromBody] Lead lead)
    {
        var updated = await _repo.UpdateAsync(id, lead);
        if (updated == null) return NotFound();
        return Ok(updated);
    }
}
