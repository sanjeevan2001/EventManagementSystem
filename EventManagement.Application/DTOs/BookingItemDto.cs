using System;

namespace EventManagement.Application.DTOs
{
    public class BookingItemDto
    {
        public Guid BookingId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
