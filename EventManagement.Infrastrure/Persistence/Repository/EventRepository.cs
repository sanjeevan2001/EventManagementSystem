using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastrure.Persistence.Repository
{
    public class EventRepository(ApplicationDbContext _context) : IEventRepository
    {
       
        public async Task<List<Event>> GetAllAsync()
            => await _context.Events.AsNoTracking().ToListAsync();

        public async Task<Event?> GetByIdAsync(Guid id)
            => await _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.EventId == id);

        public async Task<List<Event>> GetByVenueIdAsync(Guid venueId)
            => await _context.Events.AsNoTracking().Where(e => e.VenueId == venueId).ToListAsync();

        public async Task AddAsync(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event ev)
        {
            _context.Events.Update(ev);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Event ev)
        {
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }
    }
}
