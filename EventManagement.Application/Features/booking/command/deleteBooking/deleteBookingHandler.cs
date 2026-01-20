using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.Interfaces.IServices;
using EventManagement.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.booking.command.deleteBooking
{
    public class deleteBookingHandler : IRequestHandler<deleteBookingCommand>
    {
        private readonly IBookingWorkflowService _workflow;
        private readonly IBookingRepository _bookingRepo;

        public deleteBookingHandler(IBookingWorkflowService workflow, IBookingRepository bookingRepo)
        {
            _workflow = workflow;
            _bookingRepo = bookingRepo;
        }

        public async Task<Unit> Handle(deleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepo.GetByIdAsync(request.Id);
            if (booking == null) throw new KeyNotFoundException("Booking not found");

            if (!request.IsAdmin)
            {
                if (request.ActingUserId == Guid.Empty) throw new UnauthorizedAccessException();
                if (booking.UserId != request.ActingUserId) throw new UnauthorizedAccessException();
            }

            await _workflow.CancelBookingAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
