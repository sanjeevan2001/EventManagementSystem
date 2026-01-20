using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.booking.Queries.GetMyBookings
{
    public record GetMyBookingsQuery(Guid UserId) : IRequest<List<BookingDto>>;
}
