using EventManagement.Application.DTOs;
using MediatR;
using System;

namespace EventManagement.Application.Features.assert.Queries.GetAssertById
{
    public record GetAssertByIdQuery(Guid AssetId) : IRequest<AssetDto?>;
}
