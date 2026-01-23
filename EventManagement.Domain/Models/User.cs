using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Email { get; set; } = "";
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; } 
        public string? Role { get; set; }
        public DateTime CreatedAt { get; set; }

        // Email Verification
        public bool IsEmailVerified { get; set; } = false;
        public string? EmailVerificationToken { get; set; }
        public DateTime? VerificationTokenExpiry { get; set; }

        // Profile Photo
        public string? PhotoUrl { get; set; }

        // Navigation
        public Admin? Admin { get; set; }
        public Client? Client { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
