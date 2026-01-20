using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class Item
    {
        [Key]
        public Guid ItemId { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }

        public Guid AssetId { get; set; }

        // Navigation
        public Asset? Asset { get; set; }
        public ICollection<PackageItem> PackageItems { get; set; } = new List<PackageItem>();
        public ICollection<BookingItem> BookingItems { get; set; } = new List<BookingItem>();

    }
}
