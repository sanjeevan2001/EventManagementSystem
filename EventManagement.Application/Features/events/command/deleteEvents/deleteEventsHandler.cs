using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using MediatR;
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
                throw new KeyNotFoundException("Event not found");

            await _repo.DeleteAsync(ev);
            return Unit.Value;
        }
    }
}
