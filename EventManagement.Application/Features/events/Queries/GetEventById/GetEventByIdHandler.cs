using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.events.Queries.GetEventById
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, EventDto?>
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;

        public GetEventByIdHandler(IEventRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EventDto?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var ev = await _repo.GetByIdAsync(request.Id);
            return ev == null ? null : _mapper.Map<EventDto>(ev);
        }
    }
}
