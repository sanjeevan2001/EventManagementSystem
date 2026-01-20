using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EventManagement.Application.Features.booking.command.deleteBooking
{
    public record deleteBookingCommand(Guid Id, Guid ActingUserId, bool IsAdmin) : IRequest;
}
