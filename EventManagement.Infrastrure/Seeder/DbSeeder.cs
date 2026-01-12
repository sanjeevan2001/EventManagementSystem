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
            // 1. Check if data already exists
            if (!context.Users.Any())
            {
                // 1. Create Admin Password
                PasswordHelper.CreatePasswordHash("Admin@123", out byte[] adminHash, out byte[] adminSalt);

                var adminUserId = Guid.NewGuid();
                var clientUserId = Guid.NewGuid();
                var venueId = Guid.NewGuid();

                // 2. Admin User
                var adminUser = new User
                {
                    UserId = adminUserId,
                    Email = "admin@example.com",
                    PasswordHash = adminHash,
                    PasswordSalt = adminSalt,
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                };

                // 3. Client Password
                PasswordHelper.CreatePasswordHash("Client@123", out byte[] clientHash, out byte[] clientSalt);

                var clientUser = new User
                {
                    UserId = clientUserId,
                    Email = "client@example.com",
                    PasswordHash = clientHash,
                    PasswordSalt = clientSalt,
                    Role = "Client",
                    CreatedAt = DateTime.UtcNow
                };

                // 4. Add Users
                context.Users.AddRange(adminUser, clientUser);

                // 5. Admin & Client Profiles
                context.Admins.Add(new Admin { UserId = adminUserId, Name = "Super Admin" });
                context.Clients.Add(new Client { UserId = clientUserId, Name = "John Doe", Address = "123 Main St", PhoneNumber = "0771234567" });

                // 6. Venue
                context.Venues.Add(new Venue { venueId = venueId, Name = "Grand Hall", Location = "Colombo", Capacity = 500, ContactInfo = "contact@grandhall.lk" });

                // 7. Save
                context.SaveChanges();
            }
        }
    }
}
