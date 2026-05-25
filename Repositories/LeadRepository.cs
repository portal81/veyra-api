using Microsoft.EntityFrameworkCore;
using VeyraApi.Interfaces;
using VeyraApi.Models;
using VeyraApi.Data;

namespace VeyraApi.Repositories;

public class LeadRepository : ILeadRepository
{
    private readonly AppDbContext _db;
    public LeadRepository(AppDbContext db) => _db = db;

    public async Task<List<Lead>> GetAllAsync() =>
        await _db.Leads.OrderByDescending(l => l.CreatedAt).ToListAsync();

    public async Task<Lead?> GetByIdAsync(string id) =>
        await _db.Leads.FirstOrDefaultAsync(l => l.Id == id);

    public async Task<Lead> CreateAsync(Lead lead)
    {
        lead.Id = $"lead-{Guid.NewGuid():N}";
        lead.CreatedAt = DateTime.UtcNow;
        _db.Leads.Add(lead);
        await _db.SaveChangesAsync();
        return lead;
    }

    public async Task<Lead?> UpdateAsync(string id, Lead lead)
    {
        var existing = await _db.Leads.FindAsync(id);
        if (existing == null) return null;
        existing.Status = lead.Status;
        existing.Stage = lead.Stage;
        existing.Priority = lead.Priority;
        existing.AssignedTo = lead.AssignedTo;
        await _db.SaveChangesAsync();
        return existing;
    }
}
