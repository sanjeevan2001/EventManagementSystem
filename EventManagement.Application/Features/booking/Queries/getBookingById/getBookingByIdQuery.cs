using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.booking.Queries.GetBookingById
{
    public record getBookingByIdQuery(Guid Id, Guid ActingUserId, bool IsAdmin) : IRequest<BookingDto?>;
}
