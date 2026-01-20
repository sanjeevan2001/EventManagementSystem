using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace EventManagement.Application.Features.bookingpackage.Queries.GetBookingPackagesForBooking
{
    public record GetBookingPackagesForBookingQuery(Guid BookingId, Guid ActingUserId, bool IsAdmin) : IRequest<List<BookingPackageDto>>;
}
