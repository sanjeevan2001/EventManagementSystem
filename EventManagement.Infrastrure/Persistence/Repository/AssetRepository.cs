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
    public class AssetRepository(ApplicationDbContext _context) : IAssetRepository
    {
        public async Task<List<Asset>> GetAllAsync()
            => await _context.Assets.AsNoTracking().ToListAsync();

        public async Task<Asset?> GetByIdAsync(Guid id)
            => await _context.Assets.AsNoTracking().FirstOrDefaultAsync(a => a.AssetId == id);

        public async Task AddAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Asset asset)
        {
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Asset asset)
        {
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
        }
    }
}
