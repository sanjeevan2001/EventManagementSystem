using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(Guid id);
        Task<List<Event>> GetByVenueIdAsync(Guid venueId);
        Task AddAsync(Event ev);
        Task UpdateAsync(Event ev);
        Task DeleteAsync(Event ev);
    }
}
