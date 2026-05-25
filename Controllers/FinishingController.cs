using Microsoft.AspNetCore.Mvc;
using VeyraApi.Interfaces;
using VeyraApi.Models;

namespace VeyraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinishingController : ControllerBase
{
    private readonly IFinishingPackageRepository _repo;
    public FinishingController(IFinishingPackageRepository repo) => _repo = repo;

    [HttpGet("packages")]
    public async Task<ActionResult<List<FinishingPackage>>> GetPackages() => Ok(await _repo.GetAllAsync());
}
