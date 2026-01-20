using EventManagement.Application.Features.venue.command.createVenue;
using EventManagement.Application.Features.venue.command.deleteVenue;
using EventManagement.Application.Features.venue.command.updateVenue;
using EventManagement.Application.Features.venue.Queries.GetAllVenues;
using EventManagement.Application.Features.venue.Queries.getVenueById;
using EventManagement.Presentation.Controllers.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class VenueController(IMediator _mediator) : BaseapiController
    {
        [HttpPost("venues")]
        public async Task<IActionResult> AddVenues(createVenueCommand command) => Ok(await _mediator.Send(command));  //  api/venues

        [HttpGet("venues")]
        public async Task<IActionResult> GetVenues() => Ok(await _mediator.Send(new GetAllVenuesQuery()));  //  api/venues

        [HttpGet("venues/{id}")]
        public async Task<IActionResult> GetVenuesrById(Guid id)   //  api/venues/{id}
        {
            var venue = await _mediator.Send(new getVenueByIdQuery(id));

            if (venue == null)
                return NotFound("Venue not found");

            return Ok(venue);
        }


        [HttpPut("venues/{id}")]
        public async Task<IActionResult> UpdateVenuesById(Guid id, updateVenueCommand command)   //  api/venues/{id}
        {
            if (command.VenueId == Guid.Empty)
                command = command with { VenueId = id };

            if (id != command.VenueId) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("venues/{id}")]
        public async Task<IActionResult> DeleteVenuesById(Guid id)   //  api/venues/{id}
        {
            await _mediator.Send(new deleteVenueCommand(id));
            return NoContent();
        }
    }
}
