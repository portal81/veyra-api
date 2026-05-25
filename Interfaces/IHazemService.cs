namespace VeyraApi.Interfaces;

public interface IHazemService
{
    Task<string> ChatAsync(string message, string sessionId);
}
