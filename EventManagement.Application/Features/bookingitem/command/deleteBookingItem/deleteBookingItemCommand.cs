using MediatR;
using System;

namespace EventManagement.Application.Features.bookingitem.command.deleteBookingItem
{
    public record deleteBookingItemCommand(
        Guid BookingId,
        Guid ItemId,
        Guid ActingUserId,
        bool IsAdmin
    ) : IRequest<Unit>;
}
