using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace EventManagement.Application.Features.bookingitem.Queries.GetBookingItemsForBooking
{
    public record GetBookingItemsForBookingQuery(Guid BookingId, Guid ActingUserId, bool IsAdmin) : IRequest<List<BookingItemDto>>;
}
