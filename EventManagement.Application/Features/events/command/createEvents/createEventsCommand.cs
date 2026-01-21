using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.events.command.createEvents
{
    public record createEventsCommand(
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        List<Guid> VenueIds,
        int MaxAttendees
    ) : IRequest<EventDto>;
}
