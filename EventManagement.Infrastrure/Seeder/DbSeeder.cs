using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using EventManagement.Infrastrure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastrure.Seeder
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            var adminEmail = "admin@example.com";
            var clientEmail = "client@example.com";

            var adminUser = context.Users.FirstOrDefault(u => u.Email == adminEmail);
            if (adminUser == null)
            {
                PasswordHelper.CreatePasswordHash("Admin@123", out byte[] adminHash, out byte[] adminSalt);
                adminUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Email = adminEmail,
                    PasswordHash = adminHash,
                    PasswordSalt = adminSalt,
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                };
                context.Users.Add(adminUser);
            }
            else
            {
                adminUser.Role ??= "Admin";
                if (adminUser.PasswordHash == null || adminUser.PasswordSalt == null)
                {
                    PasswordHelper.CreatePasswordHash("Admin@123", out byte[] adminHash, out byte[] adminSalt);
                    adminUser.PasswordHash = adminHash;
                    adminUser.PasswordSalt = adminSalt;
                }
            }

            if (!context.Admins.Any(a => a.UserId == adminUser.UserId))
            {
                context.Admins.Add(new Admin { UserId = adminUser.UserId, Name = "Super Admin" });
            }

            var clientUser = context.Users.FirstOrDefault(u => u.Email == clientEmail);
            if (clientUser == null)
            {
                PasswordHelper.CreatePasswordHash("Client@123", out byte[] clientHash, out byte[] clientSalt);
                clientUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Email = clientEmail,
                    PasswordHash = clientHash,
                    PasswordSalt = clientSalt,
                    Role = "Client",
                    CreatedAt = DateTime.UtcNow
                };
                context.Users.Add(clientUser);
            }
            else
            {
                clientUser.Role ??= "Client";
                if (clientUser.PasswordHash == null || clientUser.PasswordSalt == null)
                {
                    PasswordHelper.CreatePasswordHash("Client@123", out byte[] clientHash, out byte[] clientSalt);
                    clientUser.PasswordHash = clientHash;
                    clientUser.PasswordSalt = clientSalt;
                }
            }

            if (!context.Clients.Any(c => c.UserId == clientUser.UserId))
            {
                context.Clients.Add(new Client { UserId = clientUser.UserId, Name = "John Doe", Address = "123 Main St", PhoneNumber = "0771234567" });
            }

            if (!context.Venues.Any())
            {
                var venueId = Guid.NewGuid();
                context.Venues.Add(new Venue { venueId = venueId, Name = "Grand Hall", Location = "Colombo", Capacity = 500, ContactInfo = "contact@grandhall.lk" });
            }

            context.SaveChanges();
        }
    }
}
