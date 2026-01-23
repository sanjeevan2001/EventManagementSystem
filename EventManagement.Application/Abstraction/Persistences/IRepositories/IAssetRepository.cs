using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IAssetRepository : IGenericRepository<Asset>
    {
        new Task<List<Asset>> GetAllAsync();
    }
}
