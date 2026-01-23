using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Application.Interfaces.IServices;
using EventManagement.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.booking.command.createBooking
{
    public class createBookingHandler : IRequestHandler<createBookingCommand, BookingDto>
    {
        private readonly IBookingWorkflowService _workflow;
        private readonly IMapper _mapper;

        public createBookingHandler(IBookingWorkflowService workflow, IMapper mapper)
        {
            _workflow = workflow;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(createBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _workflow.CreateBookingAsync(
                request.EventId,
                request.UserId,
                request.AttendeesCount,
                request.Packages,
                request.Items,
                cancellationToken);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
