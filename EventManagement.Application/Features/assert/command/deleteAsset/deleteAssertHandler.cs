using EventManagement.Application.Abstraction.Persistences.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.assert.command.deleteAsset
{
    public class deleteAssertHandler : IRequestHandler<deleteAssetCommand, Unit>
    {
        private readonly IAssetRepository _repo;

        public deleteAssertHandler(IAssetRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(deleteAssetCommand request, CancellationToken cancellationToken)
        {
            if (request.AssetId == Guid.Empty) throw new ArgumentException("AssetId is required");

            var asset = await _repo.GetByIdAsync(request.AssetId);
            if (asset == null) throw new KeyNotFoundException("Asset not found");

            await _repo.DeleteAsync(asset);
            return Unit.Value;
        }
    }
}
