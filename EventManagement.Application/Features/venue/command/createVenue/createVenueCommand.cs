using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.venue.command.createVenue
{
    public record createVenueCommand(string Name, string Location, int Capacity, string ContactInfo) : IRequest<VenueDto>;
   
}
