namespace VeyraApi.Models;

public class Project
{
    public string Id { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string HeroImage { get; set; } = string.Empty;
    public List<string> Gallery { get; set; } = new();
    public double StartingPricePerMeter { get; set; }
    public int InstallmentYears { get; set; }
    public bool Featured { get; set; }
    public List<string> Highlights { get; set; } = new();
    public List<Unit> Units { get; set; } = new();
}
