using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EventManagement.Application.Features.events.command.deleteEvents
{
    public record deleteEventsCommand(Guid Id) : IRequest;
}
