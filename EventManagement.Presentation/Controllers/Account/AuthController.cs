using EventManagement.Application.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers.Account
{
    public class AuthController(AuthService auth) : BaseapiController
    {
        public IActionResult Index()
        {
            return Ok();
        }
        // POST api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null) return BadRequest();

            var user = auth.Login(request.Email, request.Password);
            if (user == null) return Unauthorized();

            return Ok(user);
        }



        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}
