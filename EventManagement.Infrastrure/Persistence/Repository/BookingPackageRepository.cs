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
    public class BookingPackageRepository(ApplicationDbContext _context) : IBookingPackageRepository
    {
        public async Task<List<BookingPackage>> GetAllAsync()
            => await _context.BookingPackages.AsNoTracking().ToListAsync();

        public async Task<List<BookingPackage>> GetByBookingIdAsync(Guid bookingId)
            => await _context.BookingPackages.AsNoTracking()
                .Where(bp => bp.BookingId == bookingId)
                .ToListAsync();

        public async Task<BookingPackage?> GetByIdAsync(Guid bookingId, Guid packageId)
            => await _context.BookingPackages.AsNoTracking()
                .FirstOrDefaultAsync(bp => bp.BookingId == bookingId && bp.PackageId == packageId);

        public async Task AddAsync(BookingPackage bookingPackage)
        {
            _context.BookingPackages.Add(bookingPackage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BookingPackage bookingPackage)
        {
            _context.BookingPackages.Remove(bookingPackage);
            await _context.SaveChangesAsync();
        }
    }
}
