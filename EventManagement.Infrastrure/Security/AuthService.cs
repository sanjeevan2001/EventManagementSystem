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
