using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.events.command.deleteEvents
{
    public class deleteEventsHandler : IRequestHandler<deleteEventsCommand>
    {
        private readonly IEventRepository _repo;

        public deleteEventsHandler(IEventRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(deleteEventsCommand request, CancellationToken cancellationToken)
        {
            var ev = await _repo.GetByIdAsync(request.Id);

            if (ev == null)
            {
                throw new NotFoundException("Event", request.Id);
            }

            // Check if event has active bookings
            if (ev.Bookings != null && ev.Bookings.Any())
            {
                throw new DependencyException("Event", "active bookings");
            }

            await _repo.DeleteAsync(ev);
            return Unit.Value;
        }
    }
}
