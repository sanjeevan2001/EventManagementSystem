using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.events.Queries.GetEvents
{
    public record GetEventsQuery : IRequest<List<EventDto>>;
}
