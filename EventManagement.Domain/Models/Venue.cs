using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class Venue
    {
        [Key]
        public Guid VenueId { get; set; }

        [Required(ErrorMessage = "Venue name is required")]
        [MaxLength(100, ErrorMessage = "Venue name cannot exceed 100 characters")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Location is required")]
        [MaxLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        public string Location { get; set; } = "";

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1")]
        public int Capacity { get; set; }

        [MaxLength(200, ErrorMessage = "Contact info cannot exceed 200 characters")]
        public string? ContactInfo { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
