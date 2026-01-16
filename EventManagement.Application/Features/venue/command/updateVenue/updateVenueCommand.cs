using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.venue.command.updateVenue
{
    public record updateVenueCommand(
    Guid VenueId,
    string Name,
    string Location,
    int Capacity,
    string ContactInfo
) : IRequest;
    
}
