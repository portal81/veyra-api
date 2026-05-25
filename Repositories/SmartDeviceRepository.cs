using Microsoft.EntityFrameworkCore;
using VeyraApi.Interfaces;
using VeyraApi.Models;
using VeyraApi.Data;

namespace VeyraApi.Repositories;

public class SmartDeviceRepository : ISmartDeviceRepository
{
    private readonly AppDbContext _db;
    public SmartDeviceRepository(AppDbContext db) => _db = db;

    public async Task<List<SmartDevice>> GetAllAsync() =>
        await _db.SmartDevices.ToListAsync();
}
