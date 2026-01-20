using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.bookingitem.Queries.GetBookingItemsForBooking
{
    public class GetBookingItemsForBookingHandler : IRequestHandler<GetBookingItemsForBookingQuery, List<BookingItemDto>>
    {
        private readonly IBookingItemRepository _repo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IMapper _mapper;

        public GetBookingItemsForBookingHandler(IBookingItemRepository repo, IBookingRepository bookingRepo, IMapper mapper)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
            _mapper = mapper;
        }

        public async Task<List<BookingItemDto>> Handle(GetBookingItemsForBookingQuery request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepo.GetByIdAsync(request.BookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");
            if (!request.IsAdmin)
            {
                if (request.ActingUserId == Guid.Empty) throw new UnauthorizedAccessException();
                if (booking.UserId != request.ActingUserId) throw new UnauthorizedAccessException();
            }

            var links = await _repo.GetByBookingIdAsync(request.BookingId);
            return _mapper.Map<List<BookingItemDto>>(links);
        }
    }
}
