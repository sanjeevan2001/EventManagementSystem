using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace EventManagement.Application.Features.package.command.createPackage
{
    public record createPackageHandler : IRequestHandler<createPackageCommand, PackageDto>
    {
        private readonly IPackageRepository _repo;
        private readonly IMapper _mapper;
        public createPackageHandler(IMapper mapper, IPackageRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PackageDto> Handle(
        createPackageCommand request,
        CancellationToken cancellationToken)
        {
            var package = new Package
            {
                PackageId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            await _repo.AddAsync(package);
            return _mapper.Map<PackageDto>(package);
        }




    }
}
