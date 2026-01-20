using EventManagement.Application.Features.packageitem.command.createPackageItem;
using EventManagement.Application.Features.packageitem.command.deletePackageItem;
using EventManagement.Application.Features.packageitem.Queries.GetPackageItemsForPackage;
using EventManagement.Presentation.Controllers.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class PackageItemController(IMediator _mediator) : BaseapiController
    {
        public record UpsertPackageItemRequest(Guid ItemId, int Quantity);

        [Authorize(Roles = "Admin")]
        [HttpPost("packages/{packageId}/items")]
        public async Task<IActionResult> UpsertPackageItem(Guid packageId, [FromBody] UpsertPackageItemRequest body)
        {
            var result = await _mediator.Send(new createPackageItemCommand(packageId, body.ItemId, body.Quantity));
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("packages/{packageId}/items/{itemId}")]
        public async Task<IActionResult> RemovePackageItem(Guid packageId, Guid itemId)
        {
            await _mediator.Send(new deletePackageItemCommand(packageId, itemId));
            return NoContent();
        }

        [Authorize(Roles = "Admin,Client")]
        [HttpGet("packages/{packageId}/items")]
        public async Task<IActionResult> GetItemsForPackage(Guid packageId)
            => Ok(await _mediator.Send(new GetPackageItemsForPackageQuery(packageId)));
    }
}
