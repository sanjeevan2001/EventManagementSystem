using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.command.updatePackage
{
    public class updatePackagevalidator : AbstractValidator<updatePackageCommand>
    {
        public updatePackagevalidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
