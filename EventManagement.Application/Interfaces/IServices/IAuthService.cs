using EventManagement.Application.DTOs.Request;
using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        User? Login(LoginDto loginDto);

        Task<(bool Success, string? Error, User? User)> RegisterAsync(RegisterDto registerDto);

        Task<(bool Success, string? Error)> VerifyEmailAsync(string token);

        Task<(bool Success, string? Error)> ResendVerificationEmailAsync(string email);
        
    }
}
