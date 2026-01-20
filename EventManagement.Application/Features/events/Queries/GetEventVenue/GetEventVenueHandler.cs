using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.events.Queries.GetEventVenue
{
    public class GetEventVenueHandler : IRequestHandler<GetEventVenueQuery, List<EventDto>>
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;

        public GetEventVenueHandler(IEventRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<EventDto>> Handle(GetEventVenueQuery request, CancellationToken cancellationToken)
        {
            var events = await _repo.GetByVenueIdAsync(request.VenueId);
            return _mapper.Map<List<EventDto>>(events);
        }
    }
}
