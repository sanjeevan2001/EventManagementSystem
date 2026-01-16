using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.DTOs
{
    public class VenueDto
    {
        public Guid VenueId { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Capacity { get; set; }
        public string ContactInfo { get; set; } = null!;
    }
}
