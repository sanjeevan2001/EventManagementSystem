using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EventManagement.Application.Features.item.command.deleteItem
{
    public record deleteItemCommand(Guid Id) : IRequest;
}
