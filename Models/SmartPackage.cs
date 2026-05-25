namespace VeyraApi.Models;

public class SmartPackage
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public List<string> Devices { get; set; } = new();
}
