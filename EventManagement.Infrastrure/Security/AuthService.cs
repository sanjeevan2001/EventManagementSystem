using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using EventManagement.Infrastrure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagement.Application.Abstraction.Persistences.IRepositories;

namespace EventManagement.Infrastrure.Security
{
    public class AuthRepository(ApplicationDbContext context) : IAuthRepository
    {
        public User? GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            var normalizedEmail = email.Trim().ToLowerInvariant();
            return context.Users.FirstOrDefault(u => u.Email == normalizedEmail);
        }

        public Task<User?> GetByIdWithDetailsAsync(Guid userId)
        {
            return context.Users.AsNoTracking()
                .Include(u => u.Admin)
                .Include(u => u.Client)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public Task<User?> GetByVerificationTokenAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return Task.FromResult<User?>(null);
            return context.Users
                .Include(u => u.Admin)
                .Include(u => u.Client)
                .FirstOrDefaultAsync(u => u.EmailVerificationToken == token);
        }

        public Task<bool> UserExistsAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return Task.FromResult(false);
            var normalizedEmail = email.Trim().ToLowerInvariant();
            return context.Users.AnyAsync(u => u.Email == normalizedEmail);
        }

        public Task AddUserAsync(User user)
        {
            context.Users.Add(user);
            return Task.CompletedTask;
        }

        public Task AddAdminAsync(Admin admin)
        {
            context.Admins.Add(admin);
            return Task.CompletedTask;
        }

        public Task AddClientAsync(Client client)
        {
            context.Clients.Add(client);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
