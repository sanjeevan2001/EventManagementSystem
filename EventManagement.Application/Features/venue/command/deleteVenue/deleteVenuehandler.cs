using EventManagement.Application.Abstraction.Persistences.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.venue.command.deleteVenue
{
    public class deleteVenuehandler : IRequestHandler<deleteVenueCommand, Unit>
    {
        private readonly IVenueRepository _repo;

        public deleteVenuehandler(IVenueRepository repo)
        {
            _repo = repo ;
        }

        public async Task<Unit> Handle(deleteVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await _repo.GetByIdAsync(request.VenueId);

            if (venue == null)
                throw new KeyNotFoundException("Venue not found");

            await _repo.DeleteAsync(venue);
            return Unit.Value;
        }
    }
}
