using EventManagement.Application.Features.item.command.createItem;
using EventManagement.Application.Features.item.command.deleteItem;
using EventManagement.Application.Features.item.command.updateItem;
using EventManagement.Application.Features.item.Queries.getItemById;
using EventManagement.Application.Features.item.Queries.GetItems;
using EventManagement.Presentation.Controllers.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class ItemController : BaseapiController
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("items")]
        public async Task<IActionResult> CreateItem(createItemCommand command)   // POST /items (Admin)
            => Ok(await _mediator.Send(command));

        [HttpGet("items")]
        public async Task<IActionResult> GetItems()     // GET /items (Admin)
        {
            return Ok(await _mediator.Send(new GetItemsQuery()));
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            var item = await _mediator.Send(new getItemByIdQuery(id));
            if (item == null) return NotFound("Item not found");
            return Ok(item);
        }

        [HttpPut("items/{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, updateItemCommand command)  // PUT /items/{id} (Admin)
        {
            if (command.ItemId == Guid.Empty)
                command = command with { ItemId = id };

            if (id != command.ItemId)
                return BadRequest("Id mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)  // DELETE /items/{id} (Admin)
        {
            await _mediator.Send(new deleteItemCommand(id));
            return NoContent();
        }

    }
}
