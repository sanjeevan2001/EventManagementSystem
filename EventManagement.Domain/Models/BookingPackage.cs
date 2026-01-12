using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class BookingPackage
    {
        [Key]
        public Guid BookingId { get; set; }
        [Key]
        public Guid PackageId { get; set; }

        public Booking Booking { get; set; }
        public Package Package { get; set; }
    }
}
