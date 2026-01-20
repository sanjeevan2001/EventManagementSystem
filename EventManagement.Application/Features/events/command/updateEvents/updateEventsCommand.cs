using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.events.command.updateEvents
{
    public record updateEventsCommand(
        Guid EventId,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        Guid VenueId,
        int MaxAttendees
    ) : IRequest<EventDto>;
}
