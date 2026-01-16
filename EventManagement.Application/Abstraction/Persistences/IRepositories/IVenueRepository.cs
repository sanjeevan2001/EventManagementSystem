using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IVenueRepository 
    {
        Task<List<Venue>> GetAllAsync();
        Task<Venue?> GetByIdAsync(Guid id);
        Task AddAsync(Venue venue);
        Task UpdateAsync(Venue venue);
        Task DeleteAsync(Venue venue);
    }
}
