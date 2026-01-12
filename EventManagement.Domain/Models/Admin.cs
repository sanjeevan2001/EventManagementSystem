using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class Admin
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public User? User { get; set; }
    }
}
