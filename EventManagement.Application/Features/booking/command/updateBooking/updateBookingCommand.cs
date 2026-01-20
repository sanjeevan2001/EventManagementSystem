using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.booking.command.updateBooking
{
    public record updateBookingCommand(
        Guid BookingId,
        string Status
    ) : IRequest<BookingDto>;
}
