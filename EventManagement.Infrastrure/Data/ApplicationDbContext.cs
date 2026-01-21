using EventManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastrure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Venue> Venues => Set<Venue>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Package> Packages => Set<Package>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingPackage> BookingPackages => Set<BookingPackage>();
        public DbSet<BookingItem> BookingItems => Set<BookingItem>();
        public DbSet<PackageItem> PackageItems => Set<PackageItem>();

        private static string? ToBase64OrNull(byte[]? value)
        {
            return value == null ? null : Convert.ToBase64String(value);
        }

        private static byte[]? FromBase64OrNull(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            try
            {
                return Convert.FromBase64String(value);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // ===============================
            // Primary Keys
            // ===============================
            modelBuilder.Entity<Admin>()
                .HasKey(a => a.UserId);

            modelBuilder.Entity<Client>()
                .HasKey(c => c.UserId);

            modelBuilder.Entity<BookingPackage>()
                .HasKey(bp => new { bp.BookingId, bp.PackageId });

            modelBuilder.Entity<BookingItem>()
                .HasKey(bi => new { bi.BookingId, bi.ItemId });

            modelBuilder.Entity<PackageItem>()
                .HasKey(pi => new { pi.PackageId, pi.ItemId });

            // ===============================
            // Event ↔ Venue (M : M)
            // ===============================
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Venues)
                .WithMany(v => v.Events)
                .UsingEntity(j => j.ToTable("EventVenues"));

            // ===============================
            // Event → Booking (1 : M)
            // ===============================
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===============================
            // User → Booking (1 : M)
            // ===============================
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===============================
            // Booking ↔ Package (M : M)
            // ===============================
            modelBuilder.Entity<BookingPackage>()
                .HasOne(bp => bp.Booking)
                .WithMany(b => b.BookingPackages)
                .HasForeignKey(bp => bp.BookingId);

            modelBuilder.Entity<BookingPackage>()
                .HasOne(bp => bp.Package)
                .WithMany(p => p.BookingPackages)
                .HasForeignKey(bp => bp.PackageId);

            modelBuilder.Entity<Asset>()
                .HasOne(a => a.Package)
                .WithMany()
                .HasForeignKey(a => a.PackageId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===============================
            // Asset → Item (1 : M)
            // ===============================
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Asset)
                .WithMany(a => a.Items)
                .HasForeignKey(i => i.AssetId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===============================
            // Package ↔ Item (M : M) via PackageItem
            // ===============================
            modelBuilder.Entity<PackageItem>()
                .HasOne(pi => pi.Package)
                .WithMany(p => p.PackageItems)
                .HasForeignKey(pi => pi.PackageId);

            modelBuilder.Entity<PackageItem>()
                .HasOne(pi => pi.Item)
                .WithMany(i => i.PackageItems)
                .HasForeignKey(pi => pi.ItemId);

            // ===============================
            // Booking ↔ Item (M : M) via BookingItem
            // ===============================
            modelBuilder.Entity<BookingItem>()
                .HasOne(bi => bi.Booking)
                .WithMany(b => b.BookingItems)
                .HasForeignKey(bi => bi.BookingId);

            modelBuilder.Entity<BookingItem>()
                .HasOne(bi => bi.Item)
                .WithMany(i => i.BookingItems)
                .HasForeignKey(bi => bi.ItemId);

            // ===============================
            // User → Admin (1 : 1)
            // ===============================
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne(u => u.Admin)
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===============================
            // User → Client (1 : 1)
            // ===============================
            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===============================
            // Unique Constraints
            // ===============================
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();


            // ===============================
            // Conversions
            // ===============================
            var byteArrayToBase64Converter = new ValueConverter<byte[]?, string?>(
                v => ToBase64OrNull(v),
                v => FromBase64OrNull(v)
            );

            var byteArrayComparer = new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<byte[]?>(
                (l, r) => ReferenceEquals(l, r) || (l != null && r != null && l.SequenceEqual(r)),
                v => v == null ? 0 : v.Aggregate(0, (a, b) => HashCode.Combine(a, b)),
                v => v == null ? null : v.ToArray()
            );

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasConversion(byteArrayToBase64Converter)
                .Metadata.SetValueComparer(byteArrayComparer);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordSalt)
                .HasConversion(byteArrayToBase64Converter)
                .Metadata.SetValueComparer(byteArrayComparer);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordSalt)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<Booking>()
                .Property(b => b.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Item>()
                .Property(i => i.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Package>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

        }
    }
}
