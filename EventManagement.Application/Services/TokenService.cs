using EventManagement.Application.Interfaces.IServices;
using EventManagement.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManagement.Application.Services
{
    public class TokenService(IConfiguration _config) : ITokenService
    {
        public string CreateToken(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
            };

            var tokenKey = _config["TokenKey"];
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new InvalidOperationException("TokenKey is missing. Add it to configuration (appsettings.Development.json / environment variables).");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tokenKey)
            );

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha512Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
