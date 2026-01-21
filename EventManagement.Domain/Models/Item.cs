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

        [Required(ErrorMessage = "Item name is required")]
        [MaxLength(100, ErrorMessage = "Item name cannot exceed 100 characters")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Item type is required")]
        [MaxLength(50, ErrorMessage = "Item type cannot exceed 50 characters")]
        public string Type { get; set; } = "";

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity available cannot be negative")]
        public int QuantityAvailable { get; set; }

        [Required(ErrorMessage = "Asset ID is required")]
        public Guid AssetId { get; set; }

        // Navigation
        public Asset? Asset { get; set; }
        public ICollection<PackageItem> PackageItems { get; set; } = new List<PackageItem>();
        public ICollection<BookingItem> BookingItems { get; set; } = new List<BookingItem>();

    }
}
