namespace VeyraApi.Models;

public class Unit
{
    public string Id { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Image { get; set; }
    public double Area { get; set; }
    public int Floor { get; set; }
    public int? Bedrooms { get; set; }
    public double Price { get; set; }
    public string Status { get; set; } = "available";
}
