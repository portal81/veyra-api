using Microsoft.EntityFrameworkCore;
using VeyraApi.Interfaces;
using VeyraApi.Models;
using VeyraApi.Data;

namespace VeyraApi.Repositories;

public class FinishingPackageRepository : IFinishingPackageRepository
{
    private readonly AppDbContext _db;
    public FinishingPackageRepository(AppDbContext db) => _db = db;

    public async Task<List<FinishingPackage>> GetAllAsync() =>
        await _db.FinishingPackages.OrderBy(p => p.PricePerMeter).ToListAsync();
}
