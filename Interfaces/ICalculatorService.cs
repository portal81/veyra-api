using VeyraApi.Models;

namespace VeyraApi.Interfaces;

public interface ICalculatorService
{
    record InstallmentResult(double TotalPrice, double DownPayment, double MonthlyPayment, int Years);
    record FinishingResult(double TotalCost, double PricePerMeter, double Area, string TierName);
    
    InstallmentResult CalculateInstallment(string unitType, double area, string planId, double downPaymentPercent);
    FinishingResult CalculateFinishing(string tierId, double area, List<string> addOnIds);
}
