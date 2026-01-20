using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.item.Queries.getItemById
{
    public class getItemByIdHandler : IRequestHandler<getItemByIdQuery, ItemDto?>
    {
        private readonly IItemRepository _repo;
        private readonly IMapper _mapper;

        public getItemByIdHandler(IItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ItemDto?> Handle(getItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _repo.GetByIdAsync(request.Id);
            return item == null ? null : _mapper.Map<ItemDto>(item);
        }
    }
}
