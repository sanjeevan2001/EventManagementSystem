using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task AddUserAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
