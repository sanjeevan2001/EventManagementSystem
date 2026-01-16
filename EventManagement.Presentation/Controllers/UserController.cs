using EventManagement.Presentation.Controllers.Account;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers
{
    public class UserController : BaseapiController
    {
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()   //  api/users
        {
            return Ok();
        }


        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(int id)   //  api/users/{id}
        {
            return Ok();
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUserById(int id)   //  api/users/{id}
        {
            return Ok();
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUserById(int id)   //  api/users/{id}
        {
            return Ok();
        }

    }
}
