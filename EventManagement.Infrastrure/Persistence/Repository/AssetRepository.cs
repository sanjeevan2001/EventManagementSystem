using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastrure.Persistence.Repository
{
    public class AssetRepository : GenericRepository<Asset>, IAssetRepository
    {
        public AssetRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Override GetAllAsync to return List<Asset>
        public new async Task<List<Asset>> GetAllAsync()
            => await _dbSet.AsNoTracking().ToListAsync();

        // Override GetByIdAsync to use AsNoTracking
        public override async Task<Asset?> GetByIdAsync(Guid id)
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(a => a.AssetId == id);

        // Override AddAsync to include SaveChanges
        public override async Task AddAsync(Asset asset)
        {
            await base.AddAsync(asset);
            await SaveChangesAsync();
        }

        // Override UpdateAsync to include SaveChanges
        public override async Task UpdateAsync(Asset asset)
        {
            await base.UpdateAsync(asset);
            await SaveChangesAsync();
        }

        // Override DeleteAsync to include SaveChanges
        public override async Task DeleteAsync(Asset asset)
        {
            await base.DeleteAsync(asset);
            await SaveChangesAsync();
        }
    }
}
