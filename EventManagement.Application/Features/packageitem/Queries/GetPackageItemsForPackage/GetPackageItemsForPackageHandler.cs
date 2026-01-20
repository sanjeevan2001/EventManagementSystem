using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.packageitem.Queries.GetPackageItemsForPackage
{
    public class GetPackageItemsForPackageHandler : IRequestHandler<GetPackageItemsForPackageQuery, List<PackageItemDto>>
    {
        private readonly IPackageItemRepository _repo;
        private readonly IMapper _mapper;

        public GetPackageItemsForPackageHandler(IPackageItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PackageItemDto>> Handle(GetPackageItemsForPackageQuery request, CancellationToken cancellationToken)
        {
            var links = await _repo.GetByPackageIdAsync(request.PackageId);
            return _mapper.Map<List<PackageItemDto>>(links);
        }
    }
}
