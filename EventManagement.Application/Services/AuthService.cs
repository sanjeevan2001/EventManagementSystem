using EventManagement.Application.DTOs.Request;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.Interfaces.IServices;
using EventManagement.Application.Security;
using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services
{
    public class AuthService(IAuthRepository authRepository, IEmailService emailService) : IAuthService
    {
        public User? Login(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password)) return null;

            var normalizedEmail = loginDto.Email.Trim().ToLowerInvariant();

            var user = authRepository.GetByEmail(normalizedEmail);
            if (user == null) return null;

            if (user.PasswordHash == null || user.PasswordSalt == null) return null;

            // Check if email is verified
            if (!user.IsEmailVerified) return null;

            bool verified = PasswordHasher.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            return verified ? user : null;
        }



        public async Task<(bool Success, string? Error, User? User)> RegisterAsync(RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Email)) return (false, "Email is required.", null);
            if (string.IsNullOrWhiteSpace(registerDto.Password)) return (false, "Password is required.", null);
            if (string.IsNullOrWhiteSpace(registerDto.Role)) return (false, "Role is required.", null);
            if (string.IsNullOrWhiteSpace(registerDto.Name)) return (false, "Name is required.", null);

            var normalizedEmail = registerDto.Email.Trim().ToLowerInvariant();
            var normalizedRole = registerDto.Role.Trim();

            var exists = await authRepository.UserExistsAsync(normalizedEmail);
            if (exists) return (false, "Email already exists.", null);

            PasswordHasher.CreatePasswordHash(registerDto.Password, out byte[] hash, out byte[] salt);

            var userId = Guid.NewGuid();
            var verificationToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString(); // Double GUID for longer token

            var user = new User
            {
                UserId = userId,
                Email = normalizedEmail,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = normalizedRole,
                CreatedAt = DateTime.UtcNow,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                VerificationTokenExpiry = DateTime.UtcNow.AddHours(24) // Token valid for 24 hours
            };

            await authRepository.AddUserAsync(user);

            if (string.Equals(normalizedRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                await authRepository.AddAdminAsync(new Admin { UserId = userId, Name = registerDto.Name.Trim() });
            }
            else if (string.Equals(normalizedRole, "Client", StringComparison.OrdinalIgnoreCase))
            {
                await authRepository.AddClientAsync(new Client
                {
                    UserId = userId,
                    Name = registerDto.Name.Trim(),
                    Address = registerDto.Address ?? string.Empty,
                    PhoneNumber = registerDto.PhoneNumber ?? string.Empty
                });
            }
            else
            {
                return (false, "Invalid role. Use 'Admin' or 'Client'.", null);
            }

            await authRepository.SaveChangesAsync();

            // Send verification email
            try
            {
                await emailService.SendVerificationEmailAsync(normalizedEmail, verificationToken, registerDto.Name);
            }
            catch (Exception)
            {
                // Log error but don't fail registration - user can resend verification
                // In production, you should log this error
            }

            return (true, null, user);
        }

        public async Task<(bool Success, string? Error)> VerifyEmailAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return (false, "Invalid verification token.");

            var user = await authRepository.GetByVerificationTokenAsync(token);
            if (user == null)
                return (false, "Invalid or expired verification token.");

            if (user.VerificationTokenExpiry == null || user.VerificationTokenExpiry < DateTime.UtcNow)
                return (false, "Verification token has expired. Please request a new one.");

            if (user.IsEmailVerified)
                return (false, "Email is already verified.");

            user.IsEmailVerified = true;
            user.EmailVerificationToken = null; // Clear token after verification
            user.VerificationTokenExpiry = null;

            await authRepository.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? Error)> ResendVerificationEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "Email is required.");

            var normalizedEmail = email.Trim().ToLowerInvariant();
            var user = authRepository.GetByEmail(normalizedEmail);

            if (user == null)
                return (false, "User not found.");

            if (user.IsEmailVerified)
                return (false, "Email is already verified.");

            // Generate new token
            var verificationToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
            user.EmailVerificationToken = verificationToken;
            user.VerificationTokenExpiry = DateTime.UtcNow.AddHours(24);

            await authRepository.SaveChangesAsync();

            // Send verification email
            try
            {
                var userName = user.Admin?.Name ?? user.Client?.Name ?? "User";
                await emailService.SendVerificationEmailAsync(normalizedEmail, verificationToken, userName);
            }
            catch (Exception)
            {
                return (false, "Failed to send verification email. Please try again later.");
            }

            return (true, null);
        }
    }
}
