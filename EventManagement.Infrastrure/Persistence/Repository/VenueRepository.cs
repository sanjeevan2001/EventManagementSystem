using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastrure.Persistence.Repository
{
    public class VenueRepository(ApplicationDbContext _context) : IVenueRepository
    {
        public async Task<List<Venue>> GetAllAsync()
            => await _context.Venues.ToListAsync();

        public async Task<Venue?> GetByIdAsync(Guid id)
            => await _context.Venues.AsNoTracking()
        .FirstOrDefaultAsync(v => v.venueId == id);

        public async Task AddAsync(Venue venue)
        {
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Venue venue)
        {
            _context.Venues.Update(venue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Venue venue)
        {
            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
        }
    }
}
