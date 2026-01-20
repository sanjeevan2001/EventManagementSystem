using EventManagement.Application.Abstraction.Persistences.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.command.deletePackage
{
    public class deletePackageHandler : IRequestHandler<deletePackageCommand>
    {
        private readonly IPackageRepository _repo;

        public deletePackageHandler(IPackageRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(
            deletePackageCommand request,
            CancellationToken cancellationToken)
        {
            var package = await _repo.GetByIdAsync(request.Id);

            if (package == null)
                throw new KeyNotFoundException("Package not found");

            await _repo.DeleteAsync(package);
            return Unit.Value;
        }
    }
}
