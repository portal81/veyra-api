using VeyraApi.Models;

namespace VeyraApi.Interfaces;

public interface ILeadRepository
{
    Task<List<Lead>> GetAllAsync();
    Task<Lead?> GetByIdAsync(string id);
    Task<Lead> CreateAsync(Lead lead);
    Task<Lead?> UpdateAsync(string id, Lead lead);
}
