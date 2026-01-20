using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.booking.Queries.GetBookingById
{
    public class getBookingByIdHandler : IRequestHandler<getBookingByIdQuery, BookingDto?>
    {
        private readonly IBookingRepository _repo;
        private readonly IMapper _mapper;

        public getBookingByIdHandler(IBookingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<BookingDto?> Handle(getBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await _repo.GetByIdAsync(request.Id);
            return booking == null ? null : _mapper.Map<BookingDto>(booking);
        }
    }
}
