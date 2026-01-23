using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task AddUserAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid userId);
        Task UpdateAsync(User user);
    }
}
