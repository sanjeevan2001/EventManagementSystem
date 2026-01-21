using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using EventManagement.Infrastrure.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace EventManagement.Infrastrure.Seeder
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            SeedUsers(context);

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var jsonPath = Path.Combine(baseDir, "Seeder", "Data", "seed-data.json");
            
            // Dev environment fallback
            if (!File.Exists(jsonPath))
            {
                 // Attempt to find it relative to execution for dev convenience
                 // Adjust depth based on actual project structure if needed
                 var candidate = Path.Combine(Directory.GetCurrentDirectory(), "..", "EventManagement.Infrastrure", "Seeder", "Data", "seed-data.json");
                 if (File.Exists(candidate)) jsonPath = candidate;
            }

            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<SeedDataPayload>(json, options);

                if (data != null)
                {
                    SeedVenues(context, data.Venues);
                    SeedPackagesAssetsItems(context, data.Packages);
                    SeedEvents(context, data.Events);
                }
            }
            else 
            {
                // Fallback to random if no JSON found (or just do nothing)
            }

            context.SaveChanges();
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            var adminEmail = "admin@example.com";
            var clientEmail = "client@example.com";
            
            // Ensure Admin
            if (!context.Users.Any(u => u.Email == adminEmail))
            {
                 PasswordHelper.CreatePasswordHash("Admin@123", out byte[] h, out byte[] s);
                 var user = new User { UserId = Guid.NewGuid(), Email = adminEmail, PasswordHash = h, PasswordSalt = s, Role = "Admin", CreatedAt = DateTime.UtcNow };
                 context.Users.Add(user);
                 context.Admins.Add(new Admin { UserId = user.UserId, Name = "Super Admin" });
            }
            
            // Ensure Client
            if (!context.Users.Any(u => u.Email == clientEmail))
            {
                 PasswordHelper.CreatePasswordHash("Client@123", out byte[] h, out byte[] s);
                 var user = new User { UserId = Guid.NewGuid(), Email = clientEmail, PasswordHash = h, PasswordSalt = s, Role = "Client", CreatedAt = DateTime.UtcNow };
                 context.Users.Add(user);
                 context.Clients.Add(new Client { UserId = user.UserId, Name = "John Doe", Address = "123 Main St", PhoneNumber = "771234567" });
            }
            context.SaveChanges();
        }

        private static void SeedVenues(ApplicationDbContext context, List<VenueJson> venues)
        {
            if (context.Venues.Any()) return; 
            
            foreach (var v in venues)
            {
                context.Venues.Add(new Venue
                {
                    VenueId = Guid.NewGuid(),
                    Name = v.Name,
                    Location = v.Location,
                    Capacity = v.Capacity,
                    ContactInfo = v.ContactInfo
                });
            }
            context.SaveChanges();
        }

        private static void SeedPackagesAssetsItems(ApplicationDbContext context, List<PackageJson> packages)
        {
            if (context.Packages.Any()) return; 

            foreach (var p in packages)
            {
                var newPkg = new Package
                {
                    PackageId = Guid.NewGuid(),
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                };
                context.Packages.Add(newPkg);

                if (p.Assets != null)
                {
                    foreach (var a in p.Assets)
                    {
                        var newAsset = new Asset
                        {
                            AssetId = Guid.NewGuid(),
                            Name = a.Name,
                            Description = a.Description,
                            QuantityAvailable = a.QuantityAvailable,
                            PackageId = newPkg.PackageId
                        };
                        context.Assets.Add(newAsset);

                        if (a.Items != null)
                        {
                             foreach (var i in a.Items)
                             {
                                 context.Items.Add(new Item
                                 {
                                     ItemId = Guid.NewGuid(),
                                     Name = i.Name,
                                     Type = i.Type,
                                     Price = i.Price,
                                     QuantityAvailable = i.QuantityAvailable,
                                     AssetId = newAsset.AssetId
                                 });
                             }
                        }
                    }
                }
            }
            context.SaveChanges();
        }

        private static void SeedEvents(ApplicationDbContext context, List<EventJson> events)
        {
             if (context.Events.Any()) return;

             var allVenues = context.Venues.ToList();

             foreach (var e in events)
             {
                 var startDate = DateTime.UtcNow.AddDays(e.DaysFromNow);
                 // Find venues by name
                 var linkedVenues = allVenues.Where(v => e.VenueNames.Contains(v.Name)).ToList();

                 var ev = new Event
                 {
                     EventId = Guid.NewGuid(),
                     Name = e.Name,
                     Description = e.Description,
                     StartDate = startDate,
                     EndDate = startDate.AddHours(e.DurationHours),
                     MaxAttendees = e.MaxAttendees,
                     Venues = linkedVenues
                 };
                 context.Events.Add(ev);
             }
             context.SaveChanges();
        }

        private class SeedDataPayload
        {
            public List<VenueJson> Venues { get; set; } = new();
            public List<PackageJson> Packages { get; set; } = new();
            public List<EventJson> Events { get; set; } = new();
        }

        private class VenueJson
        {
            public string Name { get; set; } = "";
            public string Location { get; set; } = "";
            public int Capacity { get; set; }
            public string ContactInfo { get; set; } = "";
        }

        private class PackageJson
        {
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public decimal Price { get; set; }
            public List<AssetJson> Assets { get; set; } = new();
        }

        private class AssetJson
        {
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public int QuantityAvailable { get; set; }
            public List<ItemJson> Items { get; set; } = new();
        }

        private class ItemJson
        {
            public string Name { get; set; } = "";
            public string Type { get; set; } = "";
            public decimal Price { get; set; }
            public int QuantityAvailable { get; set; }
        }

        private class EventJson
        {
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public List<string> VenueNames { get; set; } = new();
            public int DaysFromNow { get; set; }
            public int DurationHours { get; set; }
            public int MaxAttendees { get; set; }
        }
    }
}
