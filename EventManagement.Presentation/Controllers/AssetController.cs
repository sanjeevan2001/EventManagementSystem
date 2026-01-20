using EventManagement.Presentation.Controllers.Account;
using EventManagement.Application.Features.assert.Queries.GetAssets;
using EventManagement.Application.Features.assert.Queries.GetAssertById;
using EventManagement.Application.Features.assert.command.createAssert;
using EventManagement.Application.Features.assert.command.updateAssert;
using EventManagement.Application.Features.assert.command.deleteAsset;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class AssetController(IMediator _mediator) : BaseapiController
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("asset")]
        [HttpPost("assets")]
        public async Task<IActionResult> CreateAsset([FromBody] createAssertCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("assets")]
        public async Task<IActionResult> GetAssets()
            => Ok(await _mediator.Send(new GetAssetsQuery()));

        [HttpGet("assets/{id}")]
        public async Task<IActionResult> GetAssetById(Guid id)
        {
            var asset = await _mediator.Send(new GetAssertByIdQuery(id));
            if (asset == null) return NotFound("Asset not found");
            return Ok(asset);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("assets/{id}")]
        public async Task<IActionResult> UpdateAsset(Guid id, [FromBody] updateAssertCommand command)
        {
            if (command.AssetId == Guid.Empty)
                command = command with { AssetId = id };

            if (id != command.AssetId)
                return BadRequest("Id mismatch");

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("assets/{id}")]
        public async Task<IActionResult> DeleteAsset(Guid id)
        {
            await _mediator.Send(new deleteAssetCommand(id));
            return NoContent();
        }

    }
}
