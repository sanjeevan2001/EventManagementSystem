using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class Asset
    {
        [Key]
        public Guid AssetId { get; set; }
        public string Name { get; set; } = "";
        public int QuantityAvailable { get; set; }
        public string Description { get; set; } = "";

        public Guid PackageId { get; set; }
        public Package? Package { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
