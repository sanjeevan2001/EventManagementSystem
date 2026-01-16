using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Application.DTOs.Request
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
        [Required]
        public string Role { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }




    }
}
