using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.booking.Queries.GetMyBookings
{
    public class GetMyBookingsHandler : IRequestHandler<GetMyBookingsQuery, List<BookingDto>>
    {
        private readonly IBookingRepository _repo;
        private readonly IMapper _mapper;

        public GetMyBookingsHandler(IBookingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<BookingDto>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _repo.GetByUserIdAsync(request.UserId);
            return _mapper.Map<List<BookingDto>>(bookings);
        }
    }
}
