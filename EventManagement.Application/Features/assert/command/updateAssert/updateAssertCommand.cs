using EventManagement.Application.DTOs;
using MediatR;
using System;

namespace EventManagement.Application.Features.assert.command.updateAssert
{
    public record updateAssertCommand(
        Guid AssetId,
        string Name,
        int QuantityAvailable,
        string Description,
        Guid PackageId
    ) : IRequest<AssetDto>;
}
