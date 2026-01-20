using FluentValidation;

namespace EventManagement.Application.Features.bookingpackage.Queries.GetBookingPackagesForBooking
{
    public class GetBookingPackagesForBookingValidator : AbstractValidator<GetBookingPackagesForBookingQuery>
    {
        public GetBookingPackagesForBookingValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty();
        }
    }
}
