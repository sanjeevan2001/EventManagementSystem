using System;

namespace EventManagement.Application.DTOs
{
    public class PackageItemDto
    {
        public Guid PackageId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
