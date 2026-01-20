using EventManagement.Application.DTOs;
using MediatR;
using System;

namespace EventManagement.Application.Features.packageitem.command.createPackageItem
{
    public record createPackageItemCommand(
        Guid PackageId,
        Guid ItemId,
        int Quantity
    ) : IRequest<PackageItemDto>;
}
