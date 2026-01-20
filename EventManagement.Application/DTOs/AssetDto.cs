using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.DTOs
{
    public class AssetDto
    {
        public Guid AssetId { get; set; }
        public string Name { get; set; } = null!;
        public int QuantityAvailable { get; set; }
        public string Description { get; set; } = null!;
        public Guid PackageId { get; set; }
    }
}
