using VeyraApi.Models;

namespace VeyraApi.Interfaces;

public interface IFinishingPackageRepository
{
    Task<List<FinishingPackage>> GetAllAsync();
}
