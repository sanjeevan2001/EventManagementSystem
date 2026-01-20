using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.bookingpackage.command.deleteBookingPackage
{
    public class deleteBookingPackageHandler : IRequestHandler<deleteBookingPackageCommand, Unit>
    {
        private readonly IBookingPackageRepository _repo;
        private readonly IBookingRepository _bookingRepo;

        public deleteBookingPackageHandler(IBookingPackageRepository repo, IBookingRepository bookingRepo)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
        }

        public async Task<Unit> Handle(deleteBookingPackageCommand request, CancellationToken cancellationToken)
        {
            if (request.BookingId == Guid.Empty) throw new ArgumentException("BookingId is required");
            if (request.PackageId == Guid.Empty) throw new ArgumentException("PackageId is required");

            var booking = await _bookingRepo.GetByIdAsync(request.BookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");
            if (booking.Status != BookingStatus.Pending)
                throw new InvalidOperationException("Booking packages can only be modified while booking status is Pending");

            var link = await _repo.GetByIdAsync(request.BookingId, request.PackageId);
            if (link == null) throw new KeyNotFoundException("BookingPackage not found");

            await _repo.DeleteAsync(link);
            return Unit.Value;
        }
    }
}
