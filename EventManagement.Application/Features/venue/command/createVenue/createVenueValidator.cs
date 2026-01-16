using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.venue.command.createVenue
{
    public class createVenueValidator : AbstractValidator<createVenueCommand>
    {
        public createVenueValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.Capacity).GreaterThan(0);
            RuleFor(x => x.ContactInfo).NotEmpty();
        }
    }
}
