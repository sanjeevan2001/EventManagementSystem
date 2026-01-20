using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.Queries.getPackageById
{
    public class getPackageByIdhandler : IRequestHandler<getPackageByIdQuery, PackageDto?>
    {
        private readonly IPackageRepository _repo;
        private readonly IMapper _mapper;

        public getPackageByIdhandler(IPackageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PackageDto?> Handle(
        getPackageByIdQuery request,
        CancellationToken cancellationToken)
        {
            var package = await _repo.GetByIdAsync(request.Id);

            if (package == null)
                return null;

            return _mapper.Map<PackageDto>(package);
        }

    }
}
