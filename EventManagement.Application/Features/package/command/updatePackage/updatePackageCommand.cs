using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.command.updatePackage
{
    public record updatePackageCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price
) : IRequest<PackageDto>;

}
