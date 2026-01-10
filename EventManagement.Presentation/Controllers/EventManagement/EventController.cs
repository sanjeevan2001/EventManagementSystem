using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers.EventManagement
{
    public class EventController : BaseapiController
    {
        [HttpGet]    // http://localhost:5241/api/index
        public IActionResult Index()
        {
            Console.Write("index...");
            return Ok();
        }
    }
}
