using EventManagement.Application.Features.events.command.createEvents;
using EventManagement.Application.Features.events.command.deleteEvents;
using EventManagement.Application.Features.events.command.updateEvents;
using EventManagement.Application.Features.events.Queries.GetEventById;
using EventManagement.Application.Features.events.Queries.GetEvents;
using EventManagement.Presentation.Controllers.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class EventController : BaseapiController
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("event")]
        [HttpPost("events")]
        public async Task<IActionResult> AddEvent(createEventsCommand command)   //  api/event
            => Ok(await _mediator.Send(command));

        [HttpGet("events")]
        public async Task<IActionResult> GetEvent()   //  api/events
        {
            return Ok(await _mediator.Send(new GetEventsQuery()));
        }

        [HttpGet("events/{id}")]
        public async Task<IActionResult> GetEventById(Guid id)   //  api/events/{id}
        {
            var ev = await _mediator.Send(new GetEventByIdQuery(id));
            if (ev == null) return NotFound("Event not found");
            return Ok(ev);
        }

        [HttpPut("event/{id}")]
        [HttpPut("events/{id}")]
        public async Task<IActionResult> UpdateEventById(Guid id, updateEventsCommand command)   //  api/events/{id}
        {
            if (command.EventId == Guid.Empty)
                command = command with { EventId = id };

            if (id != command.EventId)
                return BadRequest("Id mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("event/{id}")]
        [HttpDelete("events/{id}")]
        public async Task<IActionResult> DeleteEventById(Guid id)   //  api/events/{id}
        {
            await _mediator.Send(new deleteEventsCommand(id));
            return NoContent();
        }
    }
}
