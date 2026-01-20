using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IPackageRepository
    {
        Task<Package> CreateAsync(Package package);
        Task<List<Package>> GetAllAsync();
        Task<Package?> GetByIdAsync(Guid id);
        Task UpdateAsync(Package package);
        Task DeleteAsync(Package package);
    }
}
