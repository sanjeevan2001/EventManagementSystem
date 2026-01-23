using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.IServices
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(Stream imageStream, string fileName);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
