using EventManagement.Application.Abstraction.Persistences.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.packageitem.command.deletePackageItem
{
    public class deletePackageItemHandler : IRequestHandler<deletePackageItemCommand, Unit>
    {
        private readonly IPackageItemRepository _repo;

        public deletePackageItemHandler(IPackageItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(deletePackageItemCommand request, CancellationToken cancellationToken)
        {
            if (request.PackageId == Guid.Empty) throw new ArgumentException("PackageId is required");
            if (request.ItemId == Guid.Empty) throw new ArgumentException("ItemId is required");

            var link = await _repo.GetByIdAsync(request.PackageId, request.ItemId);
            if (link == null) throw new KeyNotFoundException("PackageItem not found");

            await _repo.DeleteAsync(link);
            return Unit.Value;
        }
    }
}
