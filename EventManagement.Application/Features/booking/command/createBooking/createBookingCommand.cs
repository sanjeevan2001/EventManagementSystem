using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.booking.command.createBooking
{
    public record createBookingCommand(
        Guid EventId,
        Guid UserId = default
    ) : IRequest<BookingDto>;
}
