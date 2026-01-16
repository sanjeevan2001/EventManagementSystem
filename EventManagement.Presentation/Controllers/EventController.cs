using EventManagement.Presentation.Controllers.Account;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    public class EventController : BaseapiController
    {

        [HttpPost("event")]
        public async Task<IActionResult> AddEvent()   //  api/event
        {
            return Ok();
        }


        [HttpGet("events")]
        public async Task<IActionResult> GetEvent()   //  api/events
        {
            return Ok();
        }


        [HttpGet("events/{id}")]
        public async Task<IActionResult> GetEventById(int id)   //  api/events/{id}
        {
            return Ok();
        }

        [HttpPut("event/{id}")]
        public async Task<IActionResult> UpdateEventById(int id)   //  api/events/{id}
        {
            return Ok();
        }

        [HttpDelete("event/{id}")]
        public async Task<IActionResult> DeleteEventById(int id)   //  api/events/{id}
        {
            return Ok();
        }
    }
}
