using EventManagement.Application.Features.package.command.createPackage;
using EventManagement.Application.Features.package.command.deletePackage;
using EventManagement.Application.Features.package.command.updatePackage;
using EventManagement.Application.Features.package.Queries.getPackageById;
using EventManagement.Application.Features.package.Queries.GetPackages;
using EventManagement.Presentation.Controllers.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class PackageController : BaseapiController
    {
        private readonly IMediator _mediator;

        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("packages")]
        public async Task<IActionResult> CreatePackage(createPackageCommand command) => Ok(await _mediator.Send(command));  // POST /packages (Admin)


        [HttpGet("packages")]
        public async Task<IActionResult> GetPackages() => Ok(await _mediator.Send(new GetPackagesQuery()));   // GET /packages (Public)


        [HttpGet("packages/{id}")]
        public async Task<IActionResult> GetPackageById(Guid id) => Ok(await _mediator.Send(new getPackageByIdQuery(id))); // GET /packages/{id} (Public)


        [HttpPut("packages/{id}")]
        public async Task<IActionResult> UpdatePackage(Guid id, updatePackageCommand command)  // PUT /packages/{id} (Admin)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("packages/{id}")]
        public async Task<IActionResult> DeletePackage(Guid id)  // DELETE /packages/{id} (Admin)
        {
            await _mediator.Send(new deletePackageCommand(id));
            return NoContent();
        }

    }
}
