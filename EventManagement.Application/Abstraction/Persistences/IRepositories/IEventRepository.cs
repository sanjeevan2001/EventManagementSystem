using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        // Override to return List instead of IEnumerable
        new Task<List<Event>> GetAllAsync();
        
        // Custom method specific to Event
        Task<List<Event>> GetByVenueIdAsync(Guid venueId);
    }
}
