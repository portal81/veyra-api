namespace VeyraApi.Models;

public class SmartDevice
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public List<string> Benefits { get; set; } = new();
}
