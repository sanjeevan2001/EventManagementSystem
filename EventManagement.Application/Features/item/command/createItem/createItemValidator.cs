using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.item.command.createItem
{
    public class createItemValidator : AbstractValidator<createItemCommand>
    {
        public createItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Item name is required")
                .MaximumLength(100).WithMessage("Item name cannot exceed 100 characters");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Item type is required")
                .MaximumLength(50).WithMessage("Item type cannot exceed 50 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.QuantityAvailable)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity available cannot be negative");

            RuleFor(x => x.AssetId)
                .NotEmpty().WithMessage("Asset ID is required");
        }
    }
}
