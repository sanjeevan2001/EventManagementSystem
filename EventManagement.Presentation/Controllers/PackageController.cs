using EventManagement.Presentation.Controllers.Account;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    public class PackageController : BaseapiController
    {
        [HttpPost("packages")]
        public async Task<IActionResult> CreatePackage()   // POST /packages (Admin)
        {
            return Ok();
        }

        [HttpGet("packages")]
        public async Task<IActionResult> GetPackages()     // GET /packages (Public)
        {
            return Ok();
        }

        [HttpGet("packages/{id}")]
        public async Task<IActionResult> GetPackageById(int id) // GET /packages/{id} (Public)
        {
            return Ok();
        }

        [HttpPut("packages/{id}")]
        public async Task<IActionResult> UpdatePackage(int id)  // PUT /packages/{id} (Admin)
        {
            return Ok();
        }

        [HttpDelete("packages/{id}")]
        public async Task<IActionResult> DeletePackage(int id)  // DELETE /packages/{id} (Admin)
        {
            return Ok();
        }

    }
}
