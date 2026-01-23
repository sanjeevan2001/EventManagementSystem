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
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Override GetAllAsync to return List<Event> and include navigation properties
        public new async Task<List<Event>> GetAllAsync()
            => await _dbSet
                .AsNoTracking()
                .Include(e => e.Venues)
                .ToListAsync();

        // Override GetByIdAsync to include navigation properties
        public override async Task<Event?> GetByIdAsync(Guid id)
            => await _dbSet
                .Include(e => e.Venues)
                .FirstOrDefaultAsync(e => e.EventId == id);

        // Custom method to get events by venue
        public async Task<List<Event>> GetByVenueIdAsync(Guid venueId)
            => await _dbSet
                .AsNoTracking()
                .Include(e => e.Venues)
                .Where(e => e.Venues.Any(v => v.VenueId == venueId))
                .ToListAsync();

        // Override AddAsync to include SaveChanges
        public override async Task AddAsync(Event ev)
        {
            await base.AddAsync(ev);
            await SaveChangesAsync();
        }

        // Override UpdateAsync to include SaveChanges
        public override async Task UpdateAsync(Event ev)
        {
            await base.UpdateAsync(ev);
            await SaveChangesAsync();
        }

        // Override DeleteAsync to include SaveChanges
        public override async Task DeleteAsync(Event ev)
        {
            await base.DeleteAsync(ev);
            await SaveChangesAsync();
        }

        public async Task<bool> IsVenueAvailableAsync(Guid venueId, DateTime start, DateTime end, Guid? excludeEventId = null)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(e => e.Venues.Any(v => v.VenueId == venueId))
                .Where(e => e.StartDate < end && e.EndDate > start); // Overlap check

            if (excludeEventId.HasValue)
            {
                query = query.Where(e => e.EventId != excludeEventId.Value);
            }

            // If any event exists, availability is false
            return !await query.AnyAsync();
        }
    }
}
