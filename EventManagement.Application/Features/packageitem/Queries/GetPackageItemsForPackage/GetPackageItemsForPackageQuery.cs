using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace EventManagement.Application.Features.packageitem.Queries.GetPackageItemsForPackage
{
    public record GetPackageItemsForPackageQuery(Guid PackageId) : IRequest<List<PackageItemDto>>;
}
