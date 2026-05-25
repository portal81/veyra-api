using VeyraApi.Interfaces;

namespace VeyraApi.Services;

public class CalculatorService : ICalculatorService
{
    private static readonly Dictionary<string, (double PricePerMeter, string Name)> UnitTypes = new()
    {
        ["residential"] = (20000, "Residential"),
        ["administrative"] = (40000, "Administrative"),
        ["penthouse"] = (35000, "Penthouse")
    };

    private static readonly Dictionary<string, (int Years, double Multiplier)> Plans = new()
    {
        ["plan-3"] = (3, 1.05),
        ["plan-5"] = (5, 1.11),
        ["plan-6"] = (6, 1.14),
        ["plan-7"] = (7, 1.18)
    };

    private static readonly Dictionary<string, (double PricePerMeter, string Name)> FinishingTiers = new()
    {
        ["finish-basic"] = (2200, "Basic"),
        ["finish-super"] = (3600, "Super Lux"),
        ["finish-ultra"] = (5200, "Ultra Super Lux")
    };

    private static readonly Dictionary<string, double> AddOns = new()
    {
        ["lighting"] = 35000,
        ["smart-prep"] = 42000,
        ["woodworks"] = 58000
    };

    public ICalculatorService.InstallmentResult CalculateInstallment(string unitType, double area, string planId, double downPaymentPercent)
    {
        if (!UnitTypes.TryGetValue(unitType, out var unit)) unit = (20000, "Residential");
        if (!Plans.TryGetValue(planId, out var plan)) plan = (6, 1.14);

        var totalPrice = area * unit.PricePerMeter * plan.Multiplier;
        var downPayment = totalPrice * (downPaymentPercent / 100);
        var monthlyPayment = (totalPrice - downPayment) / (plan.Years * 12);

        return new(totalPrice, downPayment, monthlyPayment, plan.Years);
    }

    public ICalculatorService.FinishingResult CalculateFinishing(string tierId, double area, List<string> addOnIds)
    {
        if (!FinishingTiers.TryGetValue(tierId, out var tier)) tier = (3600, "Super Lux");
        var totalCost = area * tier.PricePerMeter;
        foreach (var id in addOnIds)
            if (AddOns.TryGetValue(id, out var price)) totalCost += price;

        return new(totalCost, tier.PricePerMeter, area, tier.Name);
    }
}
