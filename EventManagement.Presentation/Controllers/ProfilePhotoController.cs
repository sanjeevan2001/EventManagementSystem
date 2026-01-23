using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Application.Interfaces.IServices;
using EventManagement.Application.Interfaces.IRepository;
using EventManagement.Domain.Models;

namespace EventManagement.Presentation.Controllers;

[ApiController]
[Route("api/auth/profile")]
[Authorize]
public class ProfilePhotoController : ControllerBase
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProfilePhotoController(ICloudinaryService cloudinaryService, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _cloudinaryService = cloudinaryService;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    private Guid GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
        return Guid.Parse(userIdClaim ?? Guid.Empty.ToString());
    }

    [HttpPost("photo")]
    public async Task<IActionResult> UploadPhoto([FromBody] UploadPhotoDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.ImageBase64) || string.IsNullOrWhiteSpace(dto.FileName))
            return BadRequest("Invalid image data.");

        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return NotFound();

        // Convert base64 to stream
        var imageBytes = Convert.FromBase64String(dto.ImageBase64);
        using var ms = new MemoryStream(imageBytes);
        var url = await _cloudinaryService.UploadImageAsync(ms, dto.FileName);
        user.PhotoUrl = url;
        await _userRepository.UpdateAsync(user);
        return Ok(new { photoUrl = url });
    }

    [HttpDelete("photo")]
    public async Task<IActionResult> DeletePhoto()
    {
        var userId = GetUserId();
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return NotFound();
        if (string.IsNullOrEmpty(user.PhotoUrl)) return NoContent();

        // Extract publicId from URL (assuming standard Cloudinary URL format)
        var uri = new Uri(user.PhotoUrl);
        var segments = uri.Segments;
        var publicId = segments[^1].TrimEnd('.','j','p','g','p','n','g'); // simplistic removal of extension
        await _cloudinaryService.DeleteImageAsync(publicId);
        user.PhotoUrl = null;
        await _userRepository.UpdateAsync(user);
        return NoContent();
    }
}

public class UploadPhotoDto
{
    public string ImageBase64 { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}
