using EventManagement.Application.DTOs;
using MediatR;
using System;

namespace EventManagement.Application.Features.assert.command.createAssert
{
    public record createAssertCommand(
        string Name,
        int QuantityAvailable,
        string Description,
        Guid PackageId
    ) : IRequest<AssetDto>;
}
