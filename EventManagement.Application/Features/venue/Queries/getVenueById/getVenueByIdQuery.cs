using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.venue.Queries.getVenueById
{
    public class getVenueByIdQuery(Guid Id) : IRequest<VenueDto?>;
   
}
