using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.booking.command.updateBooking
{
    public class updateBookingHandler : IRequestHandler<updateBookingCommand, BookingDto>
    {
        private readonly IBookingRepository _repo;
        private readonly IMapper _mapper;

        public updateBookingHandler(IBookingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(updateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _repo.GetByIdAsync(request.BookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");

            booking.Status = request.Status;
            await _repo.UpdateAsync(booking);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
