using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        // Override to return List instead of IEnumerable
        new Task<List<Booking>> GetAllAsync();

        Task<List<Booking>> GetByUserIdAsync(Guid userId);
        Task<List<Booking>> GetByEventIdAsync(Guid eventId);
    }
}
