using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.item.Queries.GetItems
{
    public record GetItemsQuery : IRequest<List<ItemDto>>;
}
