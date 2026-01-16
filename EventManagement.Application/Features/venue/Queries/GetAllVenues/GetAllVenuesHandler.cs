using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.venue.Queries.GetAllVenues
{
    public class GetAllVenuesHandler(IVenueRepository _repo, IMapper _mapper) : IRequestHandler<GetAllVenuesQuery, List<VenueDto>>
    {
        public async Task<List<VenueDto>> Handle(GetAllVenuesQuery request, CancellationToken ct)
        {
            var venues = await _repo.GetAllAsync();
            return _mapper.Map<List<VenueDto>>(venues);
        }
    }
}
