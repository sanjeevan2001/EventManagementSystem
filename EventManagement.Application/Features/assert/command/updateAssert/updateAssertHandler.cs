using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.assert.command.updateAssert
{
    public class updateAssertHandler : IRequestHandler<updateAssertCommand, AssetDto>
    {
        private readonly IAssetRepository _repo;
        private readonly IMapper _mapper;

        public updateAssertHandler(IAssetRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AssetDto> Handle(updateAssertCommand request, CancellationToken cancellationToken)
        {
            if (request.AssetId == Guid.Empty) throw new ArgumentException("AssetId is required");
            if (string.IsNullOrWhiteSpace(request.Name)) throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(request.Description)) throw new ArgumentException("Description is required");
            if (request.QuantityAvailable < 0) throw new ArgumentException("QuantityAvailable must be >= 0");
            if (request.PackageId == Guid.Empty) throw new ArgumentException("PackageId is required");

            var asset = await _repo.GetByIdAsync(request.AssetId);
            if (asset == null) throw new KeyNotFoundException("Asset not found");

            asset.Name = request.Name.Trim();
            asset.Description = request.Description.Trim();
            asset.QuantityAvailable = request.QuantityAvailable;
            asset.PackageId = request.PackageId;

            await _repo.UpdateAsync(asset);
            return _mapper.Map<AssetDto>(asset);
        }
    }
}
