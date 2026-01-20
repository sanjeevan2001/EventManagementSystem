using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IPackageItemRepository
    {
        Task<List<PackageItem>> GetByPackageIdAsync(Guid packageId);
        Task<PackageItem?> GetByIdAsync(Guid packageId, Guid itemId);
        Task AddAsync(PackageItem packageItem);
        Task UpdateAsync(PackageItem packageItem);
        Task DeleteAsync(PackageItem packageItem);
    }
}
