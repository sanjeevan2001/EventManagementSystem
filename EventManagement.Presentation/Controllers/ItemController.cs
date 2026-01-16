using EventManagement.Presentation.Controllers.Account;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    public class ItemController : BaseapiController
    {
        [HttpPost("items")]
        public async Task<IActionResult> CreateItem()   // POST /items (Admin)
        {
            return Ok();
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItems()     // GET /items (Admin)
        {
            return Ok();
        }

        [HttpPut("items/{id}")]
        public async Task<IActionResult> UpdateItem(int id)  // PUT /items/{id} (Admin)
        {
            return Ok();
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem(int id)  // DELETE /items/{id} (Admin)
        {
            return Ok();
        }

    }
}
