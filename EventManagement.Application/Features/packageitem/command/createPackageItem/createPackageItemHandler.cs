using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.packageitem.command.createPackageItem
{
    public class createPackageItemHandler : IRequestHandler<createPackageItemCommand, PackageItemDto>
    {
        private readonly IPackageItemRepository _repo;
        private readonly IPackageRepository _packageRepo;
        private readonly IItemRepository _itemRepo;
        private readonly IMapper _mapper;

        public createPackageItemHandler(
            IPackageItemRepository repo,
            IPackageRepository packageRepo,
            IItemRepository itemRepo,
            IMapper mapper)
        {
            _repo = repo;
            _packageRepo = packageRepo;
            _itemRepo = itemRepo;
            _mapper = mapper;
        }

        public async Task<PackageItemDto> Handle(createPackageItemCommand request, CancellationToken cancellationToken)
        {
            if (request.PackageId == Guid.Empty) throw new ArgumentException("PackageId is required");
            if (request.ItemId == Guid.Empty) throw new ArgumentException("ItemId is required");
            if (request.Quantity <= 0) throw new ArgumentException("Quantity must be greater than zero");

            var pkg = await _packageRepo.GetByIdAsync(request.PackageId);
            if (pkg == null) throw new KeyNotFoundException("Package not found");

            var item = await _itemRepo.GetByIdAsync(request.ItemId);
            if (item == null) throw new KeyNotFoundException("Item not found");

            var existing = await _repo.GetByIdAsync(request.PackageId, request.ItemId);
            if (existing != null)
            {
                var updated = new PackageItem
                {
                    PackageId = request.PackageId,
                    ItemId = request.ItemId,
                    Quantity = request.Quantity
                };

                await _repo.UpdateAsync(updated);
                return _mapper.Map<PackageItemDto>(updated);
            }

            var link = new PackageItem
            {
                PackageId = request.PackageId,
                ItemId = request.ItemId,
                Quantity = request.Quantity
            };

            await _repo.AddAsync(link);
            return _mapper.Map<PackageItemDto>(link);
        }
    }
}
