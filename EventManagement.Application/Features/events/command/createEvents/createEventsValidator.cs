using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Features.events.command.createEvents
{
    public class createEventsValidator : AbstractValidator<createEventsCommand>
    {
        public createEventsValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Event name is required")
                .MaximumLength(200).WithMessage("Event name cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Event description is required")
                .MaximumLength(1000).WithMessage("Event description cannot exceed 1000 characters");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required")
                .GreaterThan(DateTime.Now).WithMessage("Start date must be in the future");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date");

            RuleFor(x => x.MaxAttendees)
                .GreaterThan(0).WithMessage("Max attendees must be greater than 0");

            RuleFor(x => x.VenueIds)
                .NotEmpty().WithMessage("At least one venue must be selected")
                .Must(venues => venues != null && venues.Any()).WithMessage("At least one venue must be selected");
        }
    }
}
