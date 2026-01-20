using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.item.Queries.GetItems
{
    public class GetItemsHandler : IRequestHandler<GetItemsQuery, List<ItemDto>>
    {
        private readonly IItemRepository _repo;
        private readonly IMapper _mapper;

        public GetItemsHandler(IItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _repo.GetAllAsync();
            return _mapper.Map<List<ItemDto>>(items);
        }
    }
}
