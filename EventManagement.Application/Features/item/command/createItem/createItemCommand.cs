using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.item.command.createItem
{
    public record createItemCommand(
        string Name,
        string Type,
        decimal Price,
        int QuantityAvailable,
        Guid AssetId
    ) : IRequest<ItemDto>;
}
