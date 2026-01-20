using EventManagement.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace EventManagement.Application.Features.assert.Queries.GetAssets
{
    public record GetAssetsQuery() : IRequest<List<AssetDto>>;
}
