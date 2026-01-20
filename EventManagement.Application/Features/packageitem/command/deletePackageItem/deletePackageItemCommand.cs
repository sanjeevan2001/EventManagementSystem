using MediatR;
using System;

namespace EventManagement.Application.Features.packageitem.command.deletePackageItem
{
    public record deletePackageItemCommand(
        Guid PackageId,
        Guid ItemId
    ) : IRequest<Unit>;
}
