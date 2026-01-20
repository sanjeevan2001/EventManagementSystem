using EventManagement.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.IServices
{
    public interface IBookingWorkflowService
    {
        Task<Booking> CreateBookingAsync(Guid eventId, Guid userId, int attendeesCount, CancellationToken cancellationToken);
        Task<Booking> UpdateBookingStatusAsync(Guid bookingId, BookingStatus newStatus, CancellationToken cancellationToken);
        Task CancelBookingAsync(Guid bookingId, CancellationToken cancellationToken);
    }
}
