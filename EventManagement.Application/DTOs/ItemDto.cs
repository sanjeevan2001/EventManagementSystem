using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.DTOs
{
    public class ItemDto
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public Guid AssetId { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public string PackageName { get; set; } = string.Empty;
    }
}
