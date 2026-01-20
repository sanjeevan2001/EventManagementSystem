using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.venue.Queries.getVenueById
{
    public class getVenueByIdHandler : IRequestHandler<getVenueByIdQuery, VenueDto?>
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IMapper _mapper;

        public getVenueByIdHandler(
            IVenueRepository venueRepository,
            IMapper mapper)
        {
            _venueRepository = venueRepository;
            _mapper = mapper;
        }

        public async Task<VenueDto?> Handle(
            getVenueByIdQuery request,
            CancellationToken cancellationToken)
        {
            var venue = await _venueRepository.GetByIdAsync(request.VenueId);

            if (venue == null)
                return null;

            return _mapper.Map<VenueDto>(venue);
        }
    }
}
