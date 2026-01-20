using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.package.command.createPackage
{
    public class createPackagevalidator : AbstractValidator<createPackageCommand>
    {
        public createPackagevalidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
