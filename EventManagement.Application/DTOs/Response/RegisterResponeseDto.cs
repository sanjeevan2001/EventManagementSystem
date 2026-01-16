using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.DTOs.Response
{
    public class RegisterResponeseDto
    {
        public bool Success { get; set; }
        public string? Error { get; set; }

        public Guid? UserId { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
