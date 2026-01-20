using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.assert.command.createAssert
{
    public class createAssertHandler : IRequestHandler<createAssertCommand, AssetDto>
    {
        private readonly IAssetRepository _repo;
        private readonly IMapper _mapper;

        public createAssertHandler(IAssetRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AssetDto> Handle(createAssertCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name)) throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(request.Description)) throw new ArgumentException("Description is required");
            if (request.QuantityAvailable < 0) throw new ArgumentException("QuantityAvailable must be >= 0");
            if (request.PackageId == Guid.Empty) throw new ArgumentException("PackageId is required");

            var asset = new Asset
            {
                AssetId = Guid.NewGuid(),
                Name = request.Name.Trim(),
                QuantityAvailable = request.QuantityAvailable,
                Description = request.Description.Trim(),
                PackageId = request.PackageId,
            };

            await _repo.AddAsync(asset);
            return _mapper.Map<AssetDto>(asset);
        }
    }
}
