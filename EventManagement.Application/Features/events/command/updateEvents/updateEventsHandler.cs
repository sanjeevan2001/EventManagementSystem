using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Application.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.events.command.updateEvents
{
    public class updateEventsHandler : IRequestHandler<updateEventsCommand, EventDto>
    {
        private readonly IEventRepository _repo;
        private readonly IVenueRepository _venueRepo;
        private readonly IMapper _mapper;

        public updateEventsHandler(IEventRepository repo, IVenueRepository venueRepo, IMapper mapper)
        {
            _repo = repo;
            _venueRepo = venueRepo;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(updateEventsCommand request, CancellationToken cancellationToken)
        {
            var ev = await _repo.GetByIdAsync(request.EventId);
            if (ev == null)
            {
                throw new NotFoundException("Event", request.EventId);
            }

            // Validate date range
            if (request.EndDate <= request.StartDate)
            {
                throw new ValidationException("End date must be after start date");
            }

            ev.Name = request.Name;
            ev.Description = request.Description;
            ev.StartDate = request.StartDate;
            ev.EndDate = request.EndDate;
            ev.MaxAttendees = request.MaxAttendees;

            // Update venues
            ev.Venues.Clear();
            foreach (var venueId in request.VenueIds)
            {
               var v = await _venueRepo.GetByIdAsync(venueId);
               if (v != null) ev.Venues.Add(v);
            }

            // Ensure at least one valid venue exists
            if (!ev.Venues.Any())
            {
                throw new ValidationException("At least one valid venue must be selected");
            }
            
            await _repo.UpdateAsync(ev);
            return _mapper.Map<EventDto>(ev);
        }
    }
}
