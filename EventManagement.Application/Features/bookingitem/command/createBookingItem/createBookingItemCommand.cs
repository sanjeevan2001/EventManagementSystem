using EventManagement.Application.DTOs;
using MediatR;
using System;

namespace EventManagement.Application.Features.bookingitem.command.createBookingItem
{
    public record createBookingItemCommand(
        Guid BookingId,
        Guid ItemId,
        int Quantity,
        Guid ActingUserId,
        bool IsAdmin
    ) : IRequest<BookingItemDto>;
}
