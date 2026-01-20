using EventManagement.Application.Features.bookingitem.command.createBookingItem;
using EventManagement.Application.Features.bookingitem.command.deleteBookingItem;
using EventManagement.Application.Features.bookingitem.Queries.GetBookingItemsForBooking;
using EventManagement.Presentation.Controllers.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class BookingItemController(IMediator _mediator) : BaseapiController
    {
        public record UpsertBookingItemRequest(Guid ItemId, int Quantity);

        [Authorize(Roles = "Admin,Client")]
        [HttpPost("bookings/{bookingId}/items")]
        public async Task<IActionResult> UpsertBookingItem(Guid bookingId, [FromBody] UpsertBookingItemRequest body)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ = Guid.TryParse(userIdValue, out var userId);
            var isAdmin = User.IsInRole("Admin");

            var result = await _mediator.Send(new createBookingItemCommand(bookingId, body.ItemId, body.Quantity, userId, isAdmin));
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Client")]
        [HttpDelete("bookings/{bookingId}/items/{itemId}")]
        public async Task<IActionResult> RemoveBookingItem(Guid bookingId, Guid itemId)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ = Guid.TryParse(userIdValue, out var userId);
            var isAdmin = User.IsInRole("Admin");

            await _mediator.Send(new deleteBookingItemCommand(bookingId, itemId, userId, isAdmin));
            return NoContent();
        }

        [Authorize(Roles = "Admin,Client")]
        [HttpGet("bookings/{bookingId}/items")]
        public async Task<IActionResult> GetItemsForBooking(Guid bookingId)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ = Guid.TryParse(userIdValue, out var userId);
            var isAdmin = User.IsInRole("Admin");

            return Ok(await _mediator.Send(new GetBookingItemsForBookingQuery(bookingId, userId, isAdmin)));
        }
    }
}
