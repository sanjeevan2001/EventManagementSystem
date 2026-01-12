using EventManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastrure.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
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

            // ===============================
            // Venue → Event (1 : M)
            // ===============================
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Venue)
                .WithMany(v => v.Events)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

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
            // Decimal Precision Configuration
            // ===============================
            modelBuilder.Entity<Item>()
                .Property(i => i.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Package>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

        }
    }
}
