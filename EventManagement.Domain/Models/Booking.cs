using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }

        public Guid UserId { get; set; }
        public Guid EventId { get; set; }

        public string? Status { get; set; }
        public DateTime BookingDate { get; set; }

        // Navigation
        public User? User { get; set; }
        public Event? Event { get; set; }
        public ICollection<BookingPackage>? BookingPackages { get; set; }
    }
}
