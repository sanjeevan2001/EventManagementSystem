using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Application.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.item.command.updateItem
{
    public class updateItemHandler : IRequestHandler<updateItemCommand, ItemDto>
    {
        private readonly IItemRepository _repo;
        private readonly IAssetRepository _assetRepo;
        private readonly IMapper _mapper;

        public updateItemHandler(IItemRepository repo, IAssetRepository assetRepo, IMapper mapper)
        {
            _repo = repo;
            _assetRepo = assetRepo;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(updateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repo.GetByIdAsync(request.ItemId);
            if (item == null)
            {
                throw new NotFoundException("Item", request.ItemId);
            }

            // Validate that the Asset exists if AssetId is being changed
            var assetExists = await _assetRepo.ExistsAsync(request.AssetId);
            if (!assetExists)
            {
                throw new NotFoundException("Asset", request.AssetId);
            }

            item.Name = request.Name;
            item.Type = request.Type;
            item.Price = request.Price;
            item.QuantityAvailable = request.QuantityAvailable;
            item.AssetId = request.AssetId;

            await _repo.UpdateAsync(item);
            return _mapper.Map<ItemDto>(item);
        }
    }
}
