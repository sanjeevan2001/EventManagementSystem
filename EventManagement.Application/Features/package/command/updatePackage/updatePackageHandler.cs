using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.command.updatePackage
{
    public class updatePackageHandler : IRequestHandler<updatePackageCommand, PackageDto>
    {
        private readonly IPackageRepository _repo;
        private readonly IMapper _mapper;

        public updatePackageHandler(IPackageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PackageDto> Handle(
            updatePackageCommand request,
            CancellationToken cancellationToken)
        {
            var package = await _repo.GetByIdAsync(request.Id);

            if (package == null)
                throw new KeyNotFoundException("Package not found");

            package.Name = request.Name;
            package.Description = request.Description;
            package.Price = request.Price;

            await _repo.UpdateAsync(package);

            return _mapper.Map<PackageDto>(package);
        }

    }
}
