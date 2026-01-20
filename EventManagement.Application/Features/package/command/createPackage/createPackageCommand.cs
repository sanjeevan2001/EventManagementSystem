using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.command.createPackage
{
    public record createPackageCommand(
    string Name,
    string Description,
    decimal Price
) : IRequest<PackageDto>;
}
