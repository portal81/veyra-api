using Microsoft.AspNetCore.Mvc;
using VeyraApi.Interfaces;

namespace VeyraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculatorsController : ControllerBase
{
    private readonly ICalculatorService _calc;
    public CalculatorsController(ICalculatorService calc) => _calc = calc;

    [HttpPost("installment")]
    public ActionResult Installment([FromBody] InstallmentRequest req) =>
        Ok(_calc.CalculateInstallment(req.UnitType, req.Area, req.PlanId, req.DownPaymentPercent));

    [HttpPost("finishing")]
    public ActionResult Finishing([FromBody] FinishingRequest req) =>
        Ok(_calc.CalculateFinishing(req.TierId, req.Area, req.AddOnIds ?? new()));
}

public record InstallmentRequest(string UnitType, double Area, string PlanId, double DownPaymentPercent);
public record FinishingRequest(string TierId, double Area, List<string>? AddOnIds);
