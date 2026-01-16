using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static EventManagement.Application.Features.venue.command.createVenue.createVenueCommand;

namespace EventManagement.Application.Features.venue.command.createVenue
{
    public class createVenueHandler(IVenueRepository _repo, IMapper _mapper) : IRequestHandler<createVenueCommand, VenueDto>
    {       
        public async Task<VenueDto> Handle(createVenueCommand request, CancellationToken ct)
        {
            var venue = new Venue
            {
                venueId = Guid.NewGuid(),
                Name = request.Name,
                Location = request.Location,
                Capacity = request.Capacity,
                ContactInfo = request.ContactInfo
            };

            await _repo.AddAsync(venue);
            return _mapper.Map<VenueDto>(venue);
        }
    }
}
