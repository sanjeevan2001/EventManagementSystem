using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        // Override to return List instead of IEnumerable
        new Task<List<Item>> GetAllAsync();
    }
}
