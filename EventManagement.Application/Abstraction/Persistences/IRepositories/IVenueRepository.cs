using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IVenueRepository : IGenericRepository<Venue>
    {
        // All basic CRUD operations are inherited from IGenericRepository<Venue>
        // Override to return List instead of IEnumerable
        new Task<List<Venue>> GetAllAsync();
    }
}
