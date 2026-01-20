using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.events.Queries.GetEventVenue
{
    public record GetEventVenueQuery(Guid VenueId) : IRequest<List<EventDto>>;
}
