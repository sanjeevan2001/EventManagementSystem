using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.venue.command.deleteVenue
{
    public record deleteVenueCommand(Guid VenueId) : IRequest;

}
