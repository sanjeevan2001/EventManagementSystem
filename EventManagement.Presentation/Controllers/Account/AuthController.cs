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
    public class AuthController(IAuthService auth, ITokenService tokenService, IAuthRepository authRepository, ICloudinaryService cloudinaryService) : BaseapiController
    {

        [HttpPost("login")]   // POST api/auth/login
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (request == null) return BadRequest();

            var user = auth.Login(request);
            if (user == null) 
            {
                // Check if user exists but email is not verified
                var existingUser = auth.Login(new LoginDto { Email = request.Email, Password = "dummy" });
                return Unauthorized(new { error = "Invalid email or password. If you registered recently, please verify your email first." });
            }

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
            var response = new 
            { 
                message = "Registration successful! Please check your email to verify your account.",
                // For Development/Demo purposes since email might fail
                debugToken = User?.EmailVerificationToken 
            };
            return Ok(response);
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
                photoUrl = user.PhotoUrl,
                createdAt = user.CreatedAt
            });
        }

        [HttpPost("profile/photo")] // POST api/auth/profile/photo
        [Authorize]
        public async Task<IActionResult> UploadProfilePhoto([FromBody] UpdateProfilePhotoDto request)
        {
            var userIdRaw = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdRaw, out var userId))
                return Unauthorized();

            var user = await authRepository.GetByIdWithDetailsAsync(userId);
            if (user == null)
                return NotFound(new { error = "User not found" });

            try
            {
                // Convert base64 to stream
                var imageBytes = Convert.FromBase64String(request.ImageBase64);
                using var imageStream = new MemoryStream(imageBytes);

                // Upload to Cloudinary
                var fileName = request.FileName ?? $"profile_{userId}.jpg";
                var photoUrl = await cloudinaryService.UploadImageAsync(imageStream, fileName);

                // Delete old photo if exists
                if (!string.IsNullOrWhiteSpace(user.PhotoUrl))
                {
                    var publicId = ExtractPublicIdFromUrl(user.PhotoUrl);
                    await cloudinaryService.DeleteImageAsync(publicId);
                }

                // Update user
                user.PhotoUrl = photoUrl;
                await authRepository.SaveChangesAsync();

                return Ok(new { photoUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Failed to upload photo: {ex.Message}" });
            }
        }

        [HttpDelete("profile/photo")] // DELETE api/auth/profile/photo
        [Authorize]
        public async Task<IActionResult> DeleteProfilePhoto()
        {
            var userIdRaw = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdRaw, out var userId))
                return Unauthorized();

            var user = await authRepository.GetByIdWithDetailsAsync(userId);
            if (user == null)
                return NotFound(new { error = "User not found" });

            if (string.IsNullOrWhiteSpace(user.PhotoUrl))
                return BadRequest(new { error = "No profile photo to delete" });

            try
            {
                var publicId = ExtractPublicIdFromUrl(user.PhotoUrl);
                await cloudinaryService.DeleteImageAsync(publicId);

                user.PhotoUrl = null;
                await authRepository.SaveChangesAsync();

                return Ok(new { message = "Profile photo deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Failed to delete photo: {ex.Message}" });
            }
        }

        private string ExtractPublicIdFromUrl(string url)
        {
            // Extract public ID from Cloudinary URL
            // URL format: https://res.cloudinary.com/{cloud_name}/image/upload/{version}/{folder}/{publicId}.{format}
            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Split('/');
            if (segments.Length >= 4)
            {
                // Get folder/publicId part (e.g., "event-management/profiles/xyz123")
                var publicIdWithFormat = string.Join("/", segments.Skip(4));
                // Remove file extension
                var lastDotIndex = publicIdWithFormat.LastIndexOf('.');
                return lastDotIndex > 0 ? publicIdWithFormat.Substring(0, lastDotIndex) : publicIdWithFormat;
            }
            return url;
        }

        [HttpPost("verify-email")] // POST api/auth/verify-email?token=xxx
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest(new { error = "Verification token is required." });

            var (Success, Error) = await auth.VerifyEmailAsync(token);
            if (!Success)
                return BadRequest(new { error = Error });

            return Ok(new { message = "Email verified successfully! You can now log in." });
        }

        [HttpPost("resend-verification")] // POST api/auth/resend-verification
        public async Task<IActionResult> ResendVerification([FromBody] ResendVerificationDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email))
                return BadRequest(new { error = "Email is required." });

            var (Success, Error) = await auth.ResendVerificationEmailAsync(request.Email);
            if (!Success)
                return BadRequest(new { error = Error });

            var user = authRepository.GetByEmail(request.Email);

            return Ok(new 
            { 
                message = "Verification email sent! Please check your inbox.",
                debugToken = user?.EmailVerificationToken 
            });
        }
    }
}
