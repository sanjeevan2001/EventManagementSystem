using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync();
        Task<Item?> GetByIdAsync(Guid id);
        Task AddAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(Item item);
    }
}
