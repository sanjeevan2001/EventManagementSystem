using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class Event
    {
        [Key]
        public Guid EventId { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        [MaxLength(200, ErrorMessage = "Event name cannot exceed 200 characters")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Max attendees must be at least 1")]
        public int MaxAttendees { get; set; }


        public ICollection<Venue> Venues { get; set; } = new List<Venue>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
