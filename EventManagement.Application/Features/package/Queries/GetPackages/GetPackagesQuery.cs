using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.Queries.GetPackages
{
    public record GetPackagesQuery : IRequest<List<PackageDto>>;
}
