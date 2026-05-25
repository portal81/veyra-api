namespace VeyraApi.Models;

public class SiteSettings
{
    public string Id { get; set; } = "primary";
    public string CompanyName { get; set; } = "Veyra Developments";
    public string PrimaryLocale { get; set; } = "en";
    public List<string> SupportedLocales { get; set; } = new() { "en", "ar" };
}
