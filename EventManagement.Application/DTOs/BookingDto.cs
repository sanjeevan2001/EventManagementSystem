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
        public string? Status { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
