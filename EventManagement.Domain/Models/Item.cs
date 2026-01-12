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
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
