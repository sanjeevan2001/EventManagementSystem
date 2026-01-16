using EventManagement.Application.Services;
using EventManagement.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Application.DTOs.Request;
using EventManagement.Domain.Models;

namespace EventManagement.Presentation.Controllers.Account
{
    public class AuthController(IAuthService auth, ITokenService tokenService) : BaseapiController
    {

        [HttpPost("login")]   // POST api/auth/login
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (request == null) return BadRequest();

            var user = auth.Login(request);
            if (user == null) return Unauthorized();

            var token = tokenService.CreateToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]  // POST api/auth/register
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (request == null) return BadRequest();
            var (Success, Error, User) = await auth.RegisterAsync(request);
            if (!Success)
            {
                return BadRequest(new { error = Error });
            }
            var token = tokenService.CreateToken(User!);
            return Ok(new { token });
        }




        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh()
        {

            return Ok();
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile() {

            return Ok();
        }





    }
}
