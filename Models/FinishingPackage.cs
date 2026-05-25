namespace VeyraApi.Models;

public class FinishingPackage
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double PricePerMeter { get; set; }
    public string Summary { get; set; } = string.Empty;
    public List<string> Features { get; set; } = new();
    public bool Featured { get; set; }
}
