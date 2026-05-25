using Microsoft.AspNetCore.Mvc;
using VeyraApi.Interfaces;
using VeyraApi.Models;

namespace VeyraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _repo;
    public ProjectsController(IProjectRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<List<Project>>> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("{slug}")]
    public async Task<ActionResult<Project>> GetBySlug(string slug)
    {
        var project = await _repo.GetBySlugAsync(slug);
        if (project == null) return NotFound();
        return Ok(project);
    }
}
