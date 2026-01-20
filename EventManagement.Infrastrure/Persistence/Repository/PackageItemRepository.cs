using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Infrastrure.Persistence.Repository
{
    public class PackageItemRepository(ApplicationDbContext _context) : IPackageItemRepository
    {
        public async Task<List<PackageItem>> GetByPackageIdAsync(Guid packageId)
            => await _context.PackageItems.AsNoTracking()
                .Where(pi => pi.PackageId == packageId)
                .ToListAsync();

        public async Task<PackageItem?> GetByIdAsync(Guid packageId, Guid itemId)
            => await _context.PackageItems.AsNoTracking()
                .FirstOrDefaultAsync(pi => pi.PackageId == packageId && pi.ItemId == itemId);

        public async Task AddAsync(PackageItem packageItem)
        {
            _context.PackageItems.Add(packageItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PackageItem packageItem)
        {
            _context.PackageItems.Update(packageItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PackageItem packageItem)
        {
            _context.PackageItems.Remove(packageItem);
            await _context.SaveChangesAsync();
        }
    }
}
