using Microsoft.EntityFrameworkCore;
using VeyraApi.Interfaces;
using VeyraApi.Models;
using VeyraApi.Data;

namespace VeyraApi.Repositories;

public class SmartPackageRepository : ISmartPackageRepository
{
    private readonly AppDbContext _db;
    public SmartPackageRepository(AppDbContext db) => _db = db;

    public async Task<List<SmartPackage>> GetAllAsync() =>
        await _db.SmartPackages.ToListAsync();
}
