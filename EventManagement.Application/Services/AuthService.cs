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
    public class AuthService(IAuthRepository authRepository) : IAuthService
    {
        public User? Login(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password)) return null;

            var normalizedEmail = loginDto.Email.Trim().ToLowerInvariant();

            var user = authRepository.GetByEmail(normalizedEmail);
            if (user == null) return null;

            if (user.PasswordHash == null || user.PasswordSalt == null) return null;

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
            var user = new User
            {
                UserId = userId,
                Email = normalizedEmail,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = normalizedRole,
                CreatedAt = DateTime.UtcNow
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
            return (true, null, user);
        }
    }
}
