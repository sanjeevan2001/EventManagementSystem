using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.events.command.createEvents
{
    public class createEventsHandler : IRequestHandler<createEventsCommand, EventDto>
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;

        public createEventsHandler(IEventRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(createEventsCommand request, CancellationToken cancellationToken)
        {
            var ev = new Event
            {
                EventId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                VenueId = request.VenueId,
                MaxAttendees = request.MaxAttendees
            };

            await _repo.AddAsync(ev);
            return _mapper.Map<EventDto>(ev);
        }
    }
}
