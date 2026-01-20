using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Domain.Models
{
    public class BookingItem
    {
        [Key]
        public Guid BookingId { get; set; }
        [Key]
        public Guid ItemId { get; set; }

        public int Quantity { get; set; }

        public Booking? Booking { get; set; }
        public Item? Item { get; set; }
    }
}
