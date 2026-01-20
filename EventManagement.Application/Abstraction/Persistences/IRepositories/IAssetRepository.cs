using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IAssetRepository
    {
        Task<List<Asset>> GetAllAsync();
        Task<Asset?> GetByIdAsync(Guid id);
        Task AddAsync(Asset asset);
        Task UpdateAsync(Asset asset);
        Task DeleteAsync(Asset asset);
    }
}
