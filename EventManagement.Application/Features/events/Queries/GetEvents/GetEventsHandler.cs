using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.events.Queries.GetEvents
{
    public class GetEventsHandler : IRequestHandler<GetEventsQuery, List<EventDto>>
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;

        public GetEventsHandler(IEventRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _repo.GetAllAsync();
            return _mapper.Map<List<EventDto>>(events);
        }
    }
}
