using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.bookingitem.command.createBookingItem
{
    public class createBookingItemHandler : IRequestHandler<createBookingItemCommand, BookingItemDto>
    {
        private readonly IBookingItemRepository _repo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IItemRepository _itemRepo;
        private readonly IMapper _mapper;

        public createBookingItemHandler(
            IBookingItemRepository repo,
            IBookingRepository bookingRepo,
            IItemRepository itemRepo,
            IMapper mapper)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
            _itemRepo = itemRepo;
            _mapper = mapper;
        }

        public async Task<BookingItemDto> Handle(createBookingItemCommand request, CancellationToken cancellationToken)
        {
            if (request.BookingId == Guid.Empty) throw new ArgumentException("BookingId is required");
            if (request.ItemId == Guid.Empty) throw new ArgumentException("ItemId is required");
            if (request.Quantity <= 0) throw new ArgumentException("Quantity must be greater than zero");

            var booking = await _bookingRepo.GetByIdAsync(request.BookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");
            if (!request.IsAdmin)
            {
                if (request.ActingUserId == Guid.Empty) throw new UnauthorizedAccessException();
                if (booking.UserId != request.ActingUserId) throw new UnauthorizedAccessException();
            }
            if (booking.Status != BookingStatus.Pending)
                throw new InvalidOperationException("Booking items can only be modified while booking status is Pending");

            var item = await _itemRepo.GetByIdAsync(request.ItemId);
            if (item == null) throw new KeyNotFoundException("Item not found");

            var existing = await _repo.GetByIdAsync(request.BookingId, request.ItemId);
            if (existing != null)
            {
                var updated = new BookingItem
                {
                    BookingId = request.BookingId,
                    ItemId = request.ItemId,
                    Quantity = request.Quantity
                };

                await _repo.UpdateAsync(updated);
                return _mapper.Map<BookingItemDto>(updated);
            }

            var link = new BookingItem
            {
                BookingId = request.BookingId,
                ItemId = request.ItemId,
                Quantity = request.Quantity
            };

            await _repo.AddAsync(link);
            return _mapper.Map<BookingItemDto>(link);
        }
    }
}
