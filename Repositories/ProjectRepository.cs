using Microsoft.EntityFrameworkCore;
using VeyraApi.Interfaces;
using VeyraApi.Models;
using VeyraApi.Data;

namespace VeyraApi.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _db;
    public ProjectRepository(AppDbContext db) => _db = db;

    public async Task<List<Project>> GetAllAsync() =>
        await _db.Projects.Include(p => p.Units).OrderByDescending(p => p.Featured).ThenBy(p => p.Name).ToListAsync();

    public async Task<Project?> GetBySlugAsync(string slug) =>
        await _db.Projects.Include(p => p.Units).FirstOrDefaultAsync(p => p.Slug == slug);
}
