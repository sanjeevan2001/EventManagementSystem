using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Application.Exceptions;
using EventManagement.Domain.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.events.command.createEvents
{
    public class createEventsHandler : IRequestHandler<createEventsCommand, EventDto>
    {
        private readonly IEventRepository _repo;
        private readonly IVenueRepository _venueRepo;
        private readonly IMapper _mapper;

        public createEventsHandler(IEventRepository repo, IVenueRepository venueRepo, IMapper mapper)
        {
            _repo = repo;
            _venueRepo = venueRepo;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(createEventsCommand request, CancellationToken cancellationToken)
        {
            // Validate date range
            if (request.EndDate <= request.StartDate)
            {
                throw new ValidationException("End date must be after start date");
            }

            // Validate and get venues
            var venues = new List<Venue>();
            foreach (var venueId in request.VenueIds)
            {
                var venue = await _venueRepo.GetByIdAsync(venueId);
                if (venue != null)
                {
                    venues.Add(venue);
                }
            }

            // Ensure at least one valid venue exists
            if (!venues.Any())
            {
                throw new ValidationException("At least one valid venue must be selected");
            }

            var ev = new Event
            {
                EventId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Venues = venues,
                MaxAttendees = request.MaxAttendees
            };

            await _repo.AddAsync(ev);
            return _mapper.Map<EventDto>(ev);
        }
    }
}
