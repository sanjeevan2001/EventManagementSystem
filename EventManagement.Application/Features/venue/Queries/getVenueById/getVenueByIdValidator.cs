using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.venue.Queries.getVenueById
{
    public class getVenueByIdValidator : AbstractValidator<getVenueByIdQuery>
    {
        public getVenueByIdValidator()
        {
            RuleFor(x => x.VenueId)
                .NotEmpty()
                .WithMessage("VenueId is required");
        }
    }
}
