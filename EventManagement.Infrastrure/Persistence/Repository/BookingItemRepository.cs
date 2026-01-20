using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Infrastrure.Persistence.Repository
{
    public class BookingItemRepository(ApplicationDbContext _context) : IBookingItemRepository
    {
        public async Task<List<BookingItem>> GetByBookingIdAsync(Guid bookingId)
            => await _context.BookingItems.AsNoTracking()
                .Where(bi => bi.BookingId == bookingId)
                .ToListAsync();

        public async Task<BookingItem?> GetByIdAsync(Guid bookingId, Guid itemId)
            => await _context.BookingItems.AsNoTracking()
                .FirstOrDefaultAsync(bi => bi.BookingId == bookingId && bi.ItemId == itemId);

        public async Task AddAsync(BookingItem bookingItem)
        {
            _context.BookingItems.Add(bookingItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BookingItem bookingItem)
        {
            _context.BookingItems.Update(bookingItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BookingItem bookingItem)
        {
            _context.BookingItems.Remove(bookingItem);
            await _context.SaveChangesAsync();
        }
    }
}
