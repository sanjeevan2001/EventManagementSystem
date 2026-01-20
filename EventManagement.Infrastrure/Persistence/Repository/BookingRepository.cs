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
    public class BookingRepository(ApplicationDbContext _context) : IBookingRepository
    {
        public async Task<List<Booking>> GetAllAsync()
            => await _context.Bookings.AsNoTracking()
                .Include(b => b.BookingPackages)
                .Include(b => b.BookingItems)
                .ToListAsync();

        public async Task<List<Booking>> GetByUserIdAsync(Guid userId)
            => await _context.Bookings.AsNoTracking()
                .Where(b => b.UserId == userId)
                .Include(b => b.BookingPackages)
                .Include(b => b.BookingItems)
                .ToListAsync();

        public async Task<Booking?> GetByIdAsync(Guid id)
            => await _context.Bookings.AsNoTracking()
                .Include(b => b.BookingPackages)
                .Include(b => b.BookingItems)
                .FirstOrDefaultAsync(b => b.BookingId == id);

        public async Task AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
