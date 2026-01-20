using MediatR;
using System;

namespace EventManagement.Application.Features.assert.command.deleteAsset
{
    public record deleteAssetCommand(Guid AssetId) : IRequest<Unit>;
}
