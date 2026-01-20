using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.booking.command.createBooking
{
    public class createBookingHandler : IRequestHandler<createBookingCommand, BookingDto>
    {
        private readonly IBookingRepository _repo;
        private readonly IMapper _mapper;

        public createBookingHandler(IBookingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(createBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                UserId = request.UserId,
                EventId = request.EventId,
                Status = "Pending",
                BookingDate = DateTime.UtcNow
            };

            await _repo.AddAsync(booking);
            return _mapper.Map<BookingDto>(booking);
        }
    }
}
