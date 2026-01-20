using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.bookingpackage.command.createBookingPackage
{
    public class createBookingPackageHandler : IRequestHandler<createBookingPackageCommand, BookingPackageDto>
    {
        private readonly IBookingPackageRepository _repo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IPackageRepository _packageRepo;
        private readonly IMapper _mapper;

        public createBookingPackageHandler(
            IBookingPackageRepository repo,
            IBookingRepository bookingRepo,
            IPackageRepository packageRepo,
            IMapper mapper)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
            _packageRepo = packageRepo;
            _mapper = mapper;
        }

        public async Task<BookingPackageDto> Handle(createBookingPackageCommand request, CancellationToken cancellationToken)
        {
            if (request.BookingId == Guid.Empty) throw new ArgumentException("BookingId is required");
            if (request.PackageId == Guid.Empty) throw new ArgumentException("PackageId is required");

            var booking = await _bookingRepo.GetByIdAsync(request.BookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");
            if (booking.Status != BookingStatus.Pending)
                throw new InvalidOperationException("Booking packages can only be modified while booking status is Pending");

            var pkg = await _packageRepo.GetByIdAsync(request.PackageId);
            if (pkg == null) throw new KeyNotFoundException("Package not found");

            var existing = await _repo.GetByIdAsync(request.BookingId, request.PackageId);
            if (existing != null) return _mapper.Map<BookingPackageDto>(existing);

            var link = new BookingPackage
            {
                BookingId = request.BookingId,
                PackageId = request.PackageId,
            };

            await _repo.AddAsync(link);
            return _mapper.Map<BookingPackageDto>(link);
        }
    }
}
