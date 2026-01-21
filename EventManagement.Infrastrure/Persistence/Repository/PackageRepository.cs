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
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        public PackageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<List<Package>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public override async Task AddAsync(Package package)
        {
            await base.AddAsync(package);
            await SaveChangesAsync();
        }

        public override async Task UpdateAsync(Package package)
        {
            await base.UpdateAsync(package);
            await SaveChangesAsync();
        }

        public override async Task DeleteAsync(Package package)
        {
            await base.DeleteAsync(package);
            await SaveChangesAsync();
        }
    }
}
