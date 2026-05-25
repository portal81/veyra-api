using Microsoft.AspNetCore.Mvc;
using VeyraApi.Interfaces;
using VeyraApi.Models;

namespace VeyraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SmartHomeController : ControllerBase
{
    private readonly ISmartDeviceRepository _devices;
    private readonly ISmartPackageRepository _packages;
    public SmartHomeController(ISmartDeviceRepository devices, ISmartPackageRepository packages) { _devices = devices; _packages = packages; }

    [HttpGet("devices")]
    public async Task<ActionResult<List<SmartDevice>>> GetDevices() => Ok(await _devices.GetAllAsync());

    [HttpGet("packages")]
    public async Task<ActionResult<List<SmartPackage>>> GetPackages() => Ok(await _packages.GetAllAsync());
}
