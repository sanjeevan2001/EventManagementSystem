using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<List<Booking>> GetByUserIdAsync(Guid userId);
        Task<Booking?> GetByIdAsync(Guid id);
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Booking booking);
    }
}
