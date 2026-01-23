using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastrure.Persistence.Repository
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Override GetAllAsync to return List<Item> and include navigation properties
        public new async Task<List<Item>> GetAllAsync()
            => await _dbSet
                .AsNoTracking()
                .Include(i => i.Asset)
                .ThenInclude(a => a!.Package!)
                .ToListAsync();

        // Override GetByIdAsync to include navigation properties and support tracking
        public override async Task<Item?> GetByIdAsync(Guid id)
            => await _dbSet
                .Include(i => i.Asset)
                .ThenInclude(a => a!.Package!)
                .FirstOrDefaultAsync(i => i.ItemId == id);

        // Override AddAsync to include SaveChanges
        public override async Task AddAsync(Item item)
        {
            await base.AddAsync(item);
            await SaveChangesAsync();
        }

        // Override UpdateAsync to include SaveChanges
        public override async Task UpdateAsync(Item item)
        {
            await base.UpdateAsync(item);
            await SaveChangesAsync();
        }

        // Override DeleteAsync to include SaveChanges
        public override async Task DeleteAsync(Item item)
        {
            await base.DeleteAsync(item);
            await SaveChangesAsync();
        }
    }
}
