using System;
using System.Collections.Generic;
using System.Text;
using EventManagement.Application.DTOs;
using MediatR;

namespace EventManagement.Application.Features.item.Queries.getItemById
{
    public record getItemByIdQuery(Guid Id) : IRequest<ItemDto?>;
}
