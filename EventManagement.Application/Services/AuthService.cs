using EventManagement.Domain.Models;
//using EventManagement.Infrastructure.Data;
using EventManagement.Infrastrure.Data;
using EventManagement.Infrastrure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace EventManagement.Application.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        // Returns User if login success, null if fail
        public User? Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            bool verified = PasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
            return verified ? user : null;
        }
    }
}
