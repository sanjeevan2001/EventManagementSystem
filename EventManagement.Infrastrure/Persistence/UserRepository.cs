using EventManagement.Application.Interfaces.IRepository;
using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EventManagement.Infrastrure.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            await base.AddAsync(user);
            await SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await base.GetByIdAsync(userId);
        }

        public async Task UpdateAsync(User user)
        {
            await base.UpdateAsync(user);
            await SaveChangesAsync();
        }
    }
}
