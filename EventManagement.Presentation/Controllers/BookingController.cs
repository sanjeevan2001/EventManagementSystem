using EventManagement.Application.Features.booking.command.createBooking;
using EventManagement.Application.Features.booking.command.deleteBooking;
using EventManagement.Application.Features.booking.command.updateBooking;
using EventManagement.Application.Features.booking.Queries.GetBookingById;
using EventManagement.Application.Features.booking.Queries.GetBookings;
using EventManagement.Application.Features.booking.Queries.GetMyBookings;
using EventManagement.Presentation.Controllers.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class BookingController : BaseapiController
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin,Client")]
        [HttpPost("bookings")]
        public async Task<IActionResult> CreateBooking(createBookingCommand command)   // POST /bookings (Client)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var result = await _mediator.Send(new createBookingCommand(command.EventId, command.AttendeesCount, userId, command.Packages, command.Items));
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookings()     // GET /bookings (Admin)
        {
            return Ok(await _mediator.Send(new GetBookingsQuery()));
        }

        [Authorize(Roles = "Client")]
        [HttpGet("bookings/my")]
        public async Task<IActionResult> GetMyBookings()   // GET /bookings/my (Client)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdValue, out var userId))
                return Unauthorized();

            return Ok(await _mediator.Send(new GetMyBookingsQuery(userId)));
        }

        [Authorize(Roles = "Admin,Client")]
        [HttpGet("bookings/{id}")]
        public async Task<IActionResult> GetBookingById(Guid id)   // GET /bookings/{id} (Admin / Client)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ = Guid.TryParse(userIdValue, out var userId);
            var isAdmin = User.IsInRole("Admin");

            var booking = await _mediator.Send(new getBookingByIdQuery(id, userId, isAdmin));
            if (booking == null) return NotFound("Booking not found");
            return Ok(booking);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("bookings/{id}/status")]
        public async Task<IActionResult> UpdateBookingStatus(Guid id, updateBookingCommand command)   // PUT /bookings/{id}/status (Admin)
        {
            if (command.BookingId == Guid.Empty)
                command = command with { BookingId = id };

            if (id != command.BookingId)
                return BadRequest("Id mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Client")]
        [HttpDelete("bookings/{id}")]
        public async Task<IActionResult> CancelBooking(Guid id)   // DELETE /bookings/{id} (Client / Admin)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ = Guid.TryParse(userIdValue, out var userId);
            var isAdmin = User.IsInRole("Admin");

            await _mediator.Send(new deleteBookingCommand(id, userId, isAdmin));
            return NoContent();
        }

    }
}
