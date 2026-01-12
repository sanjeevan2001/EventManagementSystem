using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class Package
    {
        [Key]
        public Guid PackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Navigation
        public ICollection<BookingPackage> BookingPackages { get; set; }
    }
}
