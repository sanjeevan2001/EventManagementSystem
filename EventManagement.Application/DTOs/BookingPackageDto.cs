using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.DTOs
{
    public class BookingPackageDto
    {
        public Guid BookingId { get; set; }
        public Guid PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public List<AssetDto> Assets { get; set; } = new();
    }
}
