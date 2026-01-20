using EventManagement.Application.Services;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Application.DTOs.Request;
using EventManagement.Domain.Models;
using System.Security.Claims;

namespace EventManagement.Presentation.Controllers.Account
{
    [Route("api/auth")]
    public class AuthController(IAuthService auth, ITokenService tokenService, IAuthRepository authRepository) : BaseapiController
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
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userIdRaw = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdRaw, out var userId))
                return Unauthorized();

            var user = await authRepository.GetByIdWithDetailsAsync(userId);
            if (user == null)
                return NotFound(new { error = "User not found" });

            var name = user.Admin?.Name ?? user.Client?.Name ?? string.Empty;
            var address = user.Client?.Address;
            var phoneNumber = user.Client?.PhoneNumber;

            return Ok(new
            {
                userId = user.UserId,
                email = user.Email,
                role = user.Role,
                name,
                address,
                phoneNumber,
                createdAt = user.CreatedAt
            });
        }
    }
}
