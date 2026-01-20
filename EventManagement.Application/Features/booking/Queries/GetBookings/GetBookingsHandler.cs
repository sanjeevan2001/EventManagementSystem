using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.booking.Queries.GetBookings
{
    public class GetBookingsHandler : IRequestHandler<GetBookingsQuery, List<BookingDto>>
    {
        private readonly IBookingRepository _repo;
        private readonly IMapper _mapper;

        public GetBookingsHandler(IBookingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<BookingDto>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _repo.GetAllAsync();
            return _mapper.Map<List<BookingDto>>(bookings);
        }
    }
}
