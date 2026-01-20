using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.bookingitem.command.deleteBookingItem
{
    public class deleteBookingItemHandler : IRequestHandler<deleteBookingItemCommand, Unit>
    {
        private readonly IBookingItemRepository _repo;
        private readonly IBookingRepository _bookingRepo;

        public deleteBookingItemHandler(IBookingItemRepository repo, IBookingRepository bookingRepo)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
        }

        public async Task<Unit> Handle(deleteBookingItemCommand request, CancellationToken cancellationToken)
        {
            if (request.BookingId == Guid.Empty) throw new ArgumentException("BookingId is required");
            if (request.ItemId == Guid.Empty) throw new ArgumentException("ItemId is required");

            var booking = await _bookingRepo.GetByIdAsync(request.BookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");
            if (!request.IsAdmin)
            {
                if (request.ActingUserId == Guid.Empty) throw new UnauthorizedAccessException();
                if (booking.UserId != request.ActingUserId) throw new UnauthorizedAccessException();
            }
            if (booking.Status != BookingStatus.Pending)
                throw new InvalidOperationException("Booking items can only be modified while booking status is Pending");

            var link = await _repo.GetByIdAsync(request.BookingId, request.ItemId);
            if (link == null) throw new KeyNotFoundException("BookingItem not found");

            await _repo.DeleteAsync(link);
            return Unit.Value;
        }
    }
}
