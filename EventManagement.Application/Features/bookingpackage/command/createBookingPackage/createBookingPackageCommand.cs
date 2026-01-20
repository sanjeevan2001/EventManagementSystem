using EventManagement.Application.DTOs;
using MediatR;
using System;

namespace EventManagement.Application.Features.bookingpackage.command.createBookingPackage
{
    public record createBookingPackageCommand(
        Guid BookingId,
        Guid PackageId
    ) : IRequest<BookingPackageDto>;
}
