using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.item.command.createItem
{
    public class createItemHandler : IRequestHandler<createItemCommand, ItemDto>
    {
        private readonly IItemRepository _repo;
        private readonly IMapper _mapper;

        public createItemHandler(IItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(createItemCommand request, CancellationToken cancellationToken)
        {
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
