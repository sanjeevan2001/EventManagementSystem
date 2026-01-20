using EventManagement.Presentation.Controllers.Account;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    [Route("api")]
    public class AssetController : BaseapiController
    {
        [HttpPost("asset")]
        public async Task<IActionResult> CreateAsset()   // POST /assets (Admin)
        {
            return Ok();
        }

        [HttpGet("assets")]
        public async Task<IActionResult> GetAssets()     // GET /assets (Admin)
        {
            return Ok();
        }

        [HttpPut("assets/{id}")]
        public async Task<IActionResult> UpdateAsset(int id)  // PUT /assets/{id} (Admin)
        {
            return Ok();
        }

        [HttpDelete("assets/{id}")]
        public async Task<IActionResult> DeleteAsset(int id)  // DELETE /assets/{id} (Admin)
        {
            return Ok();
        }

    }
}
