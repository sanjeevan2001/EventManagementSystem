using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using EventManagement.Infrastrure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement.Infrastrure.Seeder
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            const int targetCount = 20;
            var rng = new Random(12345);

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

            var currentAdmins = context.Admins.Count();
            if (currentAdmins < targetCount)
            {
                var toAdd = targetCount - currentAdmins;
                var added = 0;
                for (var i = 1; added < toAdd; i++)
                {
                    var email = $"admin{i}@example.com";
                    if (email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase)) continue;
                    if (context.Users.Any(u => u.Email == email)) continue;

                    PasswordHelper.CreatePasswordHash("Admin@123", out byte[] hash, out byte[] salt);
                    var user = new User
                    {
                        UserId = Guid.NewGuid(),
                        Email = email,
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Role = "Admin",
                        CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(1, 365))
                    };
                    context.Users.Add(user);
                    context.Admins.Add(new Admin { UserId = user.UserId, Name = $"Admin {i}" });
                    added++;
                }
            }

            var currentClients = context.Clients.Count();
            if (currentClients < targetCount)
            {
                var toAdd = targetCount - currentClients;
                var added = 0;
                for (var i = 1; added < toAdd; i++)
                {
                    var email = $"client{i}@example.com";
                    if (email.Equals(clientEmail, StringComparison.OrdinalIgnoreCase)) continue;
                    if (context.Users.Any(u => u.Email == email)) continue;

                    PasswordHelper.CreatePasswordHash("Client@123", out byte[] hash, out byte[] salt);
                    var user = new User
                    {
                        UserId = Guid.NewGuid(),
                        Email = email,
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Role = "Client",
                        CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(1, 365))
                    };
                    context.Users.Add(user);
                    context.Clients.Add(new Client
                    {
                        UserId = user.UserId,
                        Name = $"Client {i}",
                        Address = $"{rng.Next(10, 999)} Main St",
                        PhoneNumber = $"07{rng.Next(10000000, 99999999)}"
                    });
                    added++;
                }
            }

            var currentVenues = context.Venues.Count();
            if (currentVenues < targetCount)
            {
                var toAdd = targetCount - currentVenues;
                for (var i = 1; i <= toAdd; i++)
                {
                    context.Venues.Add(new Venue
                    {
                        VenueId = Guid.NewGuid(),
                        Name = $"Venue {currentVenues + i}",
                        Location = rng.Next(0, 2) == 0 ? "Colombo" : "Kandy",
                        Capacity = rng.Next(50, 1500),
                        ContactInfo = $"venue{currentVenues + i}@example.com"
                    });
                }
            }

            var venues = context.Venues.Select(v => v.VenueId).ToList();

            var currentEvents = context.Events.Count();
            if (currentEvents < targetCount && venues.Count > 0)
            {
                var toAdd = targetCount - currentEvents;
                for (var i = 1; i <= toAdd; i++)
                {
                    var start = DateTime.UtcNow.AddDays(rng.Next(1, 120));
                    context.Events.Add(new Event
                    {
                        EventId = Guid.NewGuid(),
                        Name = $"Event {currentEvents + i}",
                        Description = $"Sample event {currentEvents + i}",
                        StartDate = start,
                        EndDate = start.AddHours(rng.Next(2, 12)),
                        VenueId = venues[rng.Next(venues.Count)],
                        MaxAttendees = rng.Next(10, 1000)
                    });
                }
            }

            var currentPackages = context.Packages.Count();
            if (currentPackages < targetCount)
            {
                var toAdd = targetCount - currentPackages;
                for (var i = 1; i <= toAdd; i++)
                {
                    context.Packages.Add(new Package
                    {
                        PackageId = Guid.NewGuid(),
                        Name = $"Package {currentPackages + i}",
                        Description = $"Sample package {currentPackages + i}",
                        Price = Math.Round((decimal)(rng.NextDouble() * 50000 + 5000), 2)
                    });
                }
            }

            var packageIds = context.Packages.Select(p => p.PackageId).ToList();

            var currentAssets = context.Assets.Count();
            if (currentAssets < targetCount && packageIds.Count > 0)
            {
                var toAdd = targetCount - currentAssets;
                for (var i = 1; i <= toAdd; i++)
                {
                    context.Assets.Add(new Asset
                    {
                        AssetId = Guid.NewGuid(),
                        Name = $"Asset {currentAssets + i}",
                        Description = $"Sample asset {currentAssets + i}",
                        QuantityAvailable = rng.Next(0, 500),
                        PackageId = packageIds[rng.Next(packageIds.Count)]
                    });
                }
            }

            var assetIds = context.Assets.Select(a => a.AssetId).ToList();

            var currentItems = context.Items.Count();
            if (currentItems < targetCount && assetIds.Count > 0)
            {
                var toAdd = targetCount - currentItems;
                var types = new[] { "Decoration", "Food", "Audio", "Lighting", "Other" };
                for (var i = 1; i <= toAdd; i++)
                {
                    context.Items.Add(new Item
                    {
                        ItemId = Guid.NewGuid(),
                        Name = $"Item {currentItems + i}",
                        Type = types[rng.Next(types.Length)],
                        Price = Math.Round((decimal)(rng.NextDouble() * 20000 + 500), 2),
                        QuantityAvailable = rng.Next(0, 200),
                        AssetId = assetIds[rng.Next(assetIds.Count)]
                    });
                }
            }

            var clientUserIds = context.Users.Where(u => u.Role == "Client").Select(u => u.UserId).ToList();
            var eventIds = context.Events.Select(e => e.EventId).ToList();
            var itemIds = context.Items.Select(i => i.ItemId).ToList();

            var currentBookings = context.Bookings.Count();
            if (currentBookings < targetCount && clientUserIds.Count > 0 && eventIds.Count > 0)
            {
                var toAdd = targetCount - currentBookings;
                for (var i = 1; i <= toAdd; i++)
                {
                    var statusPick = rng.Next(0, 3);
                    var status = statusPick == 0 ? BookingStatus.Pending : statusPick == 1 ? BookingStatus.Confirmed : BookingStatus.Cancelled;
                    context.Bookings.Add(new Booking
                    {
                        BookingId = Guid.NewGuid(),
                        UserId = clientUserIds[rng.Next(clientUserIds.Count)],
                        EventId = eventIds[rng.Next(eventIds.Count)],
                        AttendeesCount = rng.Next(1, 500),
                        Status = status,
                        BookingDate = DateTime.UtcNow.AddDays(-rng.Next(0, 120))
                    });
                }
            }

            context.SaveChanges();

            var existingPackageItems = context.PackageItems
                .Select(pi => new { pi.PackageId, pi.ItemId })
                .ToList();
            var packageItemKeys = new HashSet<(Guid PackageId, Guid ItemId)>(existingPackageItems.Select(x => (x.PackageId, x.ItemId)));

            var packageItemTarget = targetCount;
            var currentPackageItems = context.PackageItems.Count();
            if (currentPackageItems < packageItemTarget && packageIds.Count > 0 && itemIds.Count > 0)
            {
                var toAdd = packageItemTarget - currentPackageItems;
                var added = 0;
                while (added < toAdd)
                {
                    var pkgId = packageIds[rng.Next(packageIds.Count)];
                    var itId = itemIds[rng.Next(itemIds.Count)];
                    if (packageItemKeys.Contains((pkgId, itId))) continue;
                    context.PackageItems.Add(new PackageItem
                    {
                        PackageId = pkgId,
                        ItemId = itId,
                        Quantity = rng.Next(1, 10)
                    });
                    packageItemKeys.Add((pkgId, itId));
                    added++;
                }
            }

            var bookingIds = context.Bookings.Select(b => b.BookingId).ToList();

            var existingBookingPackages = context.BookingPackages
                .Select(bp => new { bp.BookingId, bp.PackageId })
                .ToList();
            var bookingPackageKeys = new HashSet<(Guid BookingId, Guid PackageId)>(existingBookingPackages.Select(x => (x.BookingId, x.PackageId)));

            var bookingPackageTarget = targetCount;
            var currentBookingPackages = context.BookingPackages.Count();
            if (currentBookingPackages < bookingPackageTarget && bookingIds.Count > 0 && packageIds.Count > 0)
            {
                var toAdd = bookingPackageTarget - currentBookingPackages;
                var added = 0;
                while (added < toAdd)
                {
                    var bId = bookingIds[rng.Next(bookingIds.Count)];
                    var pId = packageIds[rng.Next(packageIds.Count)];
                    if (bookingPackageKeys.Contains((bId, pId))) continue;
                    context.BookingPackages.Add(new BookingPackage { BookingId = bId, PackageId = pId });
                    bookingPackageKeys.Add((bId, pId));
                    added++;
                }
            }

            var existingBookingItems = context.BookingItems
                .Select(bi => new { bi.BookingId, bi.ItemId })
                .ToList();
            var bookingItemKeys = new HashSet<(Guid BookingId, Guid ItemId)>(existingBookingItems.Select(x => (x.BookingId, x.ItemId)));

            var bookingItemTarget = targetCount;
            var currentBookingItems = context.BookingItems.Count();
            if (currentBookingItems < bookingItemTarget && bookingIds.Count > 0 && itemIds.Count > 0)
            {
                var toAdd = bookingItemTarget - currentBookingItems;
                var added = 0;
                while (added < toAdd)
                {
                    var bId = bookingIds[rng.Next(bookingIds.Count)];
                    var itId = itemIds[rng.Next(itemIds.Count)];
                    if (bookingItemKeys.Contains((bId, itId))) continue;
                    context.BookingItems.Add(new BookingItem { BookingId = bId, ItemId = itId, Quantity = rng.Next(1, 10) });
                    bookingItemKeys.Add((bId, itId));
                    added++;
                }
            }

            context.SaveChanges();
        }
    }
}
