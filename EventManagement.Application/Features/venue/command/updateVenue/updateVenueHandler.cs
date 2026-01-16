using EventManagement.Application.Abstraction.Persistences.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EventManagement.Application.Features.venue.command.updateVenue
{
    public class updateVenueHandler(IVenueRepository _repo) : IRequestHandler<updateVenueCommand, Unit>
    {
        

        public async Task<Unit> Handle(updateVenueCommand request, CancellationToken ct)
        {
            var venue = await _repo.GetByIdAsync(request.VenueId);
            if (venue == null) throw new Exception("Venue not found");

            venue.Name = request.Name;
            venue.Location = request.Location;
            venue.Capacity = request.Capacity;
            venue.ContactInfo = request.ContactInfo;

            await _repo.UpdateAsync(venue);
            return Unit.Value;
        }

    }
}
