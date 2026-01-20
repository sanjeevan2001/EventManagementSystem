using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Domain.Models
{
    public class PackageItem
    {
        [Key]
        public Guid PackageId { get; set; }
        [Key]
        public Guid ItemId { get; set; }

        public int Quantity { get; set; }

        public Package? Package { get; set; }
        public Item? Item { get; set; }
    }
}
