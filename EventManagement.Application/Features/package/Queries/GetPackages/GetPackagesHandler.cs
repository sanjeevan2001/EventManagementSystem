using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.Queries.GetPackages
{
    public class GetPackagesHandler : IRequestHandler<GetPackagesQuery, List<PackageDto>>
    {
        private readonly IPackageRepository _repo;
        private readonly IMapper _mapper;

        public GetPackagesHandler(IPackageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PackageDto>> Handle(
            GetPackagesQuery request,
            CancellationToken cancellationToken)
        {
            var packages = await _repo.GetAllAsync();
            return _mapper.Map<List<PackageDto>>(packages);
        }
    }
}
