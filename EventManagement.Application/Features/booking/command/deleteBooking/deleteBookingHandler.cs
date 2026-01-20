using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.booking.command.deleteBooking
{
    public class deleteBookingHandler : IRequestHandler<deleteBookingCommand>
    {
        private readonly IBookingRepository _repo;

        public deleteBookingHandler(IBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(deleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _repo.GetByIdAsync(request.Id);
            if (booking == null) throw new KeyNotFoundException("Booking not found");

            await _repo.DeleteAsync(booking);
            return Unit.Value;
        }
    }
}
