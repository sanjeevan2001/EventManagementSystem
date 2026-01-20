using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IBookingItemRepository
    {
        Task<List<BookingItem>> GetByBookingIdAsync(Guid bookingId);
        Task<BookingItem?> GetByIdAsync(Guid bookingId, Guid itemId);
        Task AddAsync(BookingItem bookingItem);
        Task UpdateAsync(BookingItem bookingItem);
        Task DeleteAsync(BookingItem bookingItem);
    }
}
