using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.assert.Queries.GetAssets
{
    public class GetAssetsHandler : IRequestHandler<GetAssetsQuery, List<AssetDto>>
    {
        private readonly IAssetRepository _repo;
        private readonly IMapper _mapper;

        public GetAssetsHandler(IAssetRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<AssetDto>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            var assets = await _repo.GetAllAsync();
            return _mapper.Map<List<AssetDto>>(assets);
        }
    }
}
