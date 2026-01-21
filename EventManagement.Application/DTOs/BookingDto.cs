using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.DTOs
{
    public class BookingDto
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public int AttendeesCount { get; set; }
        public string? Status { get; set; }
        public DateTime BookingDate { get; set; }

        // Enriched fields for display
        public string? EventName { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }

        public List<BookingPackageDto> BookingPackages { get; set; } = new();
        public List<BookingItemDto> BookingItems { get; set; } = new();
    }
}
