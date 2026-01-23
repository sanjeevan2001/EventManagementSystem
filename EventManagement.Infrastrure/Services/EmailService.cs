using EventManagement.Application.Configuration;
using EventManagement.Application.Interfaces.IServices;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastrure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendVerificationEmailAsync(string email, string token, string userName)
        {
            var subject = "Verify Your Email Address";
            var verificationLink = $"http://localhost:5500/verify-email?token={token}"; // Update with your frontend URL

            var body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 10px;
        }}
        .header {{
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 30px;
            text-align: center;
            border-radius: 10px 10px 0 0;
        }}
        .content {{
            background-color: white;
            padding: 30px;
            border-radius: 0 0 10px 10px;
        }}
        .button {{
            display: inline-block;
            padding: 12px 30px;
            margin: 20px 0;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white !important;
            text-decoration: none;
            border-radius: 5px;
            font-weight: bold;
        }}
        .footer {{
            text-align: center;
            margin-top: 20px;
            color: #666;
            font-size: 12px;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>Email Verification</h1>
        </div>
        <div class=""content"">
            <p>Hello {userName},</p>
            <p>Thank you for registering with Event Management System! Please verify your email address by clicking the button below:</p>
            <div style=""text-align: center;"">
                <a href=""{verificationLink}"" class=""button"">Verify Email Address</a>
            </div>
            <p>Or copy and paste this link into your browser:</p>
            <p style=""word-break: break-all; color: #667eea;"">{verificationLink}</p>
            <p>This verification link will expire in 24 hours.</p>
            <p>If you didn't create an account, you can safely ignore this email.</p>
            <p>Best regards,<br>Event Management System Team</p>
        </div>
        <div class=""footer"">
            <p>&copy; 2026 Event Management System. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port);
            smtpClient.EnableSsl = _emailSettings.EnableSsl;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
