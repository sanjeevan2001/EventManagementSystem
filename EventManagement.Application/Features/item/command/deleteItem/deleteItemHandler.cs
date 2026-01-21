using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.item.command.deleteItem
{
    public class deleteItemHandler : IRequestHandler<deleteItemCommand>
    {
        private readonly IItemRepository _repo;

        public deleteItemHandler(IItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(deleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repo.GetByIdAsync(request.Id);
            if (item == null)
            {
                throw new NotFoundException("Item", request.Id);
            }

            // Check if item is used in any active bookings
            if (item.BookingItems != null && item.BookingItems.Any())
            {
                throw new DependencyException("Item", "active bookings");
            }

            await _repo.DeleteAsync(item);
            return Unit.Value;
        }
    }
}
