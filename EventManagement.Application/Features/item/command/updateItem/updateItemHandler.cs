using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.item.command.updateItem
{
    public class updateItemHandler : IRequestHandler<updateItemCommand, ItemDto>
    {
        private readonly IItemRepository _repo;
        private readonly IMapper _mapper;

        public updateItemHandler(IItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(updateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repo.GetByIdAsync(request.ItemId);
            if (item == null) throw new KeyNotFoundException("Item not found");

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
