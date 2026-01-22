using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.DTOs
{
    public class PackageDto
    {
        public Guid PackageId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public ICollection<PackageItemDto> PackageItems { get; set; } = new List<PackageItemDto>();
    }
}
