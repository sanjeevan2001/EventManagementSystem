using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IAuthRepository
    {
        User? GetByEmail(string email);
        Task<User?> GetByIdWithDetailsAsync(Guid userId);
        Task<User?> GetByVerificationTokenAsync(string token);
        Task<bool> UserExistsAsync(string email);
        Task AddUserAsync(User user);
        Task AddAdminAsync(Admin admin);
        Task AddClientAsync(Client client);
        Task SaveChangesAsync();
    }
}
