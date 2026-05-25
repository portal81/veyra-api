using VeyraApi.Models;

namespace VeyraApi.Interfaces;

public interface ISmartPackageRepository
{
    Task<List<SmartPackage>> GetAllAsync();
}
