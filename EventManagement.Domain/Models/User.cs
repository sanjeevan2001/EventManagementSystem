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
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Admin Admin { get; set; }
        public Client Client { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
