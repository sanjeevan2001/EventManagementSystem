using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastrure.Persistence.Repository
{
    public class VenueRepository : GenericRepository<Venue>, IVenueRepository
    {
        public VenueRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Override GetAllAsync to return List<Venue> instead of IEnumerable<Venue>
        public new async Task<List<Venue>> GetAllAsync()
            => await _dbSet.ToListAsync();

        // Override GetByIdAsync to use tracking for potential updates
        public override async Task<Venue?> GetByIdAsync(Guid id)
            => await _dbSet
                .Include(v => v.Events) // Include events
                .FirstOrDefaultAsync(v => v.VenueId == id);

        // Override AddAsync to include SaveChanges
        public override async Task AddAsync(Venue venue)
        {
            await base.AddAsync(venue);
            await SaveChangesAsync();
        }

        // Override UpdateAsync to include SaveChanges
        public override async Task UpdateAsync(Venue venue)
        {
            await base.UpdateAsync(venue);
            await SaveChangesAsync();
        }

        // Override DeleteAsync to include SaveChanges
        public override async Task DeleteAsync(Venue venue)
        {
            await base.DeleteAsync(venue);
            await SaveChangesAsync();
        }
    }
}
