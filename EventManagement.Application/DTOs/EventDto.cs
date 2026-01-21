using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.DTOs
{
    public class EventDto
    {
        public Guid EventId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
        public int MaxAttendees { get; set; }
        public List<VenueDto> Venues { get; set; } = new List<VenueDto>();
    }
}
