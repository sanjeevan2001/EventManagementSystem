using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventManagement.Application.DTOs.Request
{
    public class UpdateProfilePhotoDto
    {
        [Required]
        public string ImageBase64 { get; set; } = string.Empty;
        
        public string? FileName { get; set; }
    }
}
