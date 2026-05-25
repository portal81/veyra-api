namespace VeyraApi.Models;

public class Lead
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Service { get; set; } = string.Empty;
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "new";
    public string Stage { get; set; } = "new";
    public string Priority { get; set; } = "medium";
    public string? AssignedTo { get; set; }
    public string? Source { get; set; }
    public double? Budget { get; set; }
}
