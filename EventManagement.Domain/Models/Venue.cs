using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class Venue
    {
        [Key]
        public Guid venueId { get; set; }
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public int Capacity { get; set; }
        public string? ContactInfo { get; set; }

        public ICollection<Event>? Events { get; set; }
    }
}
