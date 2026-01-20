using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.events.command.updateEvents
{
    public class updateEventsHandler : IRequestHandler<updateEventsCommand, EventDto>
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;

        public updateEventsHandler(IEventRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(updateEventsCommand request, CancellationToken cancellationToken)
        {
            var ev = await _repo.GetByIdAsync(request.EventId);
            if (ev == null) throw new KeyNotFoundException("Event not found");

            ev.Name = request.Name;
            ev.Description = request.Description;
            ev.StartDate = request.StartDate;
            ev.EndDate = request.EndDate;
            ev.VenueId = request.VenueId;
            ev.MaxAttendees = request.MaxAttendees;

            await _repo.UpdateAsync(ev);
            return _mapper.Map<EventDto>(ev);
        }
    }
}
