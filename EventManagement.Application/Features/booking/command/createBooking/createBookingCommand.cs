using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.booking.command.createBooking
{
    public record createBookingCommand(
        Guid EventId,
        int AttendeesCount = 1,
        Guid UserId = default,
        List<BookingPackageRequest>? Packages = null,
        List<BookingItemRequest>? Items = null
    ) : IRequest<BookingDto>;

    public record BookingPackageRequest(Guid PackageId);
    public record BookingItemRequest(Guid ItemId, int Quantity);
}
