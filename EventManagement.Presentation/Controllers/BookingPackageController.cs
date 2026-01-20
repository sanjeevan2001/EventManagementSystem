using EventManagement.Presentation.Controllers.Account;
using EventManagement.Application.Features.bookingpackage.command.createBookingPackage;
using EventManagement.Application.Features.bookingpackage.command.deleteBookingPackage;
using EventManagement.Application.Features.bookingpackage.Queries.GetBookingPackagesForBooking;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class BookingPackageController(IMediator _mediator) : BaseapiController
    {
        public record AssignPackageRequest(Guid PackageId);

        [Authorize(Roles = "Admin")]
        [HttpPost("bookings/{bookingId}/packages")]
        public async Task<IActionResult> AssignPackageToBooking(Guid bookingId, [FromBody] AssignPackageRequest body)
        {
            var result = await _mediator.Send(new createBookingPackageCommand(bookingId, body.PackageId));
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("bookings/{bookingId}/packages/{packageId}")]
        public async Task<IActionResult> RemovePackageFromBooking(Guid bookingId, Guid packageId)
        {
            await _mediator.Send(new deleteBookingPackageCommand(bookingId, packageId));
            return NoContent();
        }

        [Authorize(Roles = "Admin,Client")]
        [HttpGet("bookings/{bookingId}/packages")]
        public async Task<IActionResult> GetPackagesForBooking(Guid bookingId)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ = Guid.TryParse(userIdValue, out var userId);
            var isAdmin = User.IsInRole("Admin");

            return Ok(await _mediator.Send(new GetBookingPackagesForBookingQuery(bookingId, userId, isAdmin)));
        }

    }
}
