using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.Queries.getPackageById
{
    public record getPackageByIdQuery(Guid Id) : IRequest<PackageDto?>;
   
}
