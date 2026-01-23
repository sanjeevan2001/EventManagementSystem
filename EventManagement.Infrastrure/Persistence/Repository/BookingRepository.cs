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
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<List<Booking>> GetAllAsync()
            => await _dbSet.AsNoTracking()
                .Include(b => b.BookingPackages)
                .Include(b => b.BookingItems)
                .ToListAsync();

        public async Task<List<Booking>> GetByUserIdAsync(Guid userId)
            => await _dbSet.AsNoTracking()
                .Where(b => b.UserId == userId)
                .Include(b => b.BookingPackages)
                    .ThenInclude(bp => bp.Package!)
                        .ThenInclude(p => p.Assets)
                            .ThenInclude(a => a.Items)
                .Include(b => b.BookingItems)
                    .ThenInclude(bi => bi.Item!)
                .ToListAsync();

        public async Task<List<Booking>> GetByEventIdAsync(Guid eventId)
            => await _dbSet.AsNoTracking()
                .Where(b => b.EventId == eventId)
                .Include(b => b.BookingPackages)
                    .ThenInclude(bp => bp.Package!)
                        .ThenInclude(p => p.Assets)
                            .ThenInclude(a => a.Items)
                .Include(b => b.BookingItems)
                    .ThenInclude(bi => bi.Item!)
                .ToListAsync();

        public override async Task<Booking?> GetByIdAsync(Guid id)
            => await _dbSet
                .Include(b => b.BookingPackages)
                .Include(b => b.BookingItems)
                .FirstOrDefaultAsync(b => b.BookingId == id);

        public override async Task AddAsync(Booking booking)
        {
            await base.AddAsync(booking);
            await SaveChangesAsync();
        }

        public override async Task UpdateAsync(Booking booking)
        {
            await base.UpdateAsync(booking);
            await SaveChangesAsync();
        }

        public override async Task DeleteAsync(Booking booking)
        {
            await base.DeleteAsync(booking);
            await SaveChangesAsync();
        }
    }
}
