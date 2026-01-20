using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IBookingPackageRepository
    {
        Task<List<BookingPackage>> GetAllAsync();
        Task<BookingPackage?> GetByIdAsync(Guid bookingId, Guid packageId);
        Task AddAsync(BookingPackage bookingPackage);
        Task DeleteAsync(BookingPackage bookingPackage);
    }
}
