using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventManagement.Application.Abstraction.Persistences.IRepositories;

namespace EventManagement.Infrastrure.Persistence.Repository
{
    public class PackageRepository(ApplicationDbContext _context) : IPackageRepository
    {
        public async Task<Package> CreateAsync(Package package)
        {
            _context.Packages.Add(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task<List<Package>> GetAllAsync()
            => await _context.Packages.ToListAsync();

        public async Task<Package?> GetByIdAsync(Guid id)
            => await _context.Packages.FindAsync(id);

        public async Task UpdateAsync(Package package)
        {
            _context.Packages.Update(package);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Package package)
        {
            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();
        }
    }
}
