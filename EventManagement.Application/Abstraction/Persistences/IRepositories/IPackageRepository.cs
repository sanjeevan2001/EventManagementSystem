using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IPackageRepository : IGenericRepository<Package>
    {
        // Override to return List instead of IEnumerable
        new Task<List<Package>> GetAllAsync();
    }
}
