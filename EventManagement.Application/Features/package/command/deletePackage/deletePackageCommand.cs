using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.command.deletePackage
{
    public record deletePackageCommand(Guid Id) : IRequest;
}
