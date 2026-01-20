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
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid VenueId { get; set; }
        public int MaxAttendees { get; set; }


        public Venue? Venue { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
