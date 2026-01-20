using EventManagement.Application.DTOs;
using MediatR;
using System;

namespace EventManagement.Application.Features.bookingpackage.Queries.GetBookingPackageById
{
    public record GetBookingPackageByIdQuery(Guid BookingId, Guid PackageId) : IRequest<BookingPackageDto?>;
}
