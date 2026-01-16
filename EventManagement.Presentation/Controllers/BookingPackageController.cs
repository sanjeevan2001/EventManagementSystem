using EventManagement.Presentation.Controllers.Account;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    public class BookingPackageController : BaseapiController
    {
        [HttpPost("bookings/{bookingId}/packages")]
        public async Task<IActionResult> AssignPackageToBooking(int bookingId)   // POST /bookings/{bookingId}/packages (Admin)
        {
            return Ok();
        }

        [HttpDelete("bookings/{bookingId}/packages/{packageId}")]
        public async Task<IActionResult> RemovePackageFromBooking(int bookingId, int packageId)   // DELETE /bookings/{bookingId}/packages/{packageId} (Admin)
        {
            return Ok();
        }

        [HttpGet("bookings/{bookingId}/packages")]
        public async Task<IActionResult> GetPackagesForBooking(int bookingId)   // GET /bookings/{bookingId}/packages (Admin / Client)
        {
            return Ok();
        }

    }
}
