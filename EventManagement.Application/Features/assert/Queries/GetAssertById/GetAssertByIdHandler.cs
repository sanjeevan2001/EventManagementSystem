using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.assert.Queries.GetAssertById
{
    public class GetAssertByIdHandler : IRequestHandler<GetAssertByIdQuery, AssetDto?>
    {
        private readonly IAssetRepository _repo;
        private readonly IMapper _mapper;

        public GetAssertByIdHandler(IAssetRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AssetDto?> Handle(GetAssertByIdQuery request, CancellationToken cancellationToken)
        {
            var asset = await _repo.GetByIdAsync(request.AssetId);
            return asset == null ? null : _mapper.Map<AssetDto>(asset);
        }
    }
}
