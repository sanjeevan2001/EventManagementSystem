using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.venue.Queries.GetAllVenues
{
    public record GetAllVenuesQuery() : IRequest<List<VenueDto>>;
}
