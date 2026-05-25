using VeyraApi.Models;

namespace VeyraApi.Interfaces;

public interface ISmartDeviceRepository
{
    Task<List<SmartDevice>> GetAllAsync();
}
