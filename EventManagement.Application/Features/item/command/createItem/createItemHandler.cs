using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Application.Exceptions;
using EventManagement.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.item.command.createItem
{
    public class createItemHandler : IRequestHandler<createItemCommand, ItemDto>
    {
        private readonly IItemRepository _repo;
        private readonly IAssetRepository _assetRepo;
        private readonly IMapper _mapper;

        public createItemHandler(IItemRepository repo, IAssetRepository assetRepo, IMapper mapper)
        {
            _repo = repo;
            _assetRepo = assetRepo;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(createItemCommand request, CancellationToken cancellationToken)
        {
            // Validate that the Asset exists
            var assetExists = await _assetRepo.ExistsAsync(request.AssetId);
            if (!assetExists)
            {
                throw new NotFoundException("Asset", request.AssetId);
            }

            var item = new Item
            {
                ItemId = Guid.NewGuid(),
                Name = request.Name,
                Type = request.Type,
                Price = request.Price,
                QuantityAvailable = request.QuantityAvailable,
                AssetId = request.AssetId
            };

            await _repo.AddAsync(item);
            return _mapper.Map<ItemDto>(item);
        }
    }
}
