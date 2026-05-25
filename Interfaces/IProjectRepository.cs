using VeyraApi.Models;

namespace VeyraApi.Interfaces;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetBySlugAsync(string slug);
}
