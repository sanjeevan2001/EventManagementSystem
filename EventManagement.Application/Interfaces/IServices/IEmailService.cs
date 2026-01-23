using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string email, string token, string userName);
        Task SendEmailAsync(string to, string subject, string body);
    }
}
