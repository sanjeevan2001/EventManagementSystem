using EventManagement.Presentation.Controllers.Account;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    public class BookingController : BaseapiController
    {
        [HttpPost("bookings")]
        public async Task<IActionResult> CreateBooking()   // POST /bookings (Client)
        {
            return Ok();
        }

        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookings()     // GET /bookings (Admin)
        {
            return Ok();
        }

        [HttpGet("bookings/my")]
        public async Task<IActionResult> GetMyBookings()   // GET /bookings/my (Client)
        {
            return Ok();
        }

        [HttpGet("bookings/{id}")]
        public async Task<IActionResult> GetBookingById(int id)   // GET /bookings/{id} (Admin / Client)
        {
            return Ok();
        }

        [HttpPut("bookings/{id}/status")]
        public async Task<IActionResult> UpdateBookingStatus(int id)   // PUT /bookings/{id}/status (Admin)
        {
            return Ok();
        }

        [HttpDelete("bookings/{id}")]
        public async Task<IActionResult> CancelBooking(int id)   // DELETE /bookings/{id} (Client / Admin)
        {
            return Ok();
        }

    }
}
