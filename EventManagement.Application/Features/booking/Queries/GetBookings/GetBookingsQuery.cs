using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.booking.Queries.GetBookings
{
    public record GetBookingsQuery : IRequest<List<BookingDto>>;
}
