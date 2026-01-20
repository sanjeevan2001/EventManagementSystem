using MediatR;
using System;

namespace EventManagement.Application.Features.bookingpackage.command.deleteBookingPackage
{
    public record deleteBookingPackageCommand(
        Guid BookingId,
        Guid PackageId
    ) : IRequest<Unit>;
}
