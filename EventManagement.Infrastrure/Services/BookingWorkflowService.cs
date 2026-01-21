using EventManagement.Application.Interfaces.IServices;
using EventManagement.Domain.Models;
using EventManagement.Infrastrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Infrastrure.Services
{
    public class BookingWorkflowService : IBookingWorkflowService
    {
        private readonly ApplicationDbContext _context;

        public BookingWorkflowService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBookingAsync(Guid eventId, Guid userId, int attendeesCount, CancellationToken cancellationToken)
        {
            if (eventId == Guid.Empty) throw new ArgumentException("EventId is required", nameof(eventId));
            if (userId == Guid.Empty) throw new ArgumentException("UserId is required", nameof(userId));
            if (attendeesCount <= 0) throw new ArgumentException("AttendeesCount must be greater than zero", nameof(attendeesCount));

            var ev = await _context.Events
                .AsNoTracking()
                .Include(e => e.Venues)
                .FirstOrDefaultAsync(e => e.EventId == eventId, cancellationToken);

            if (ev == null) throw new KeyNotFoundException("Event not found");
            if (ev.Venues == null || !ev.Venues.Any()) throw new KeyNotFoundException("Venue not found for the event");

            var totalVenueCapacity = ev.Venues.Sum(v => v.Capacity);
            var capacityLimit = Math.Min(ev.MaxAttendees, totalVenueCapacity);

            var existingAttendees = await _context.Bookings
                .AsNoTracking()
                .Where(b => b.EventId == eventId && b.Status != BookingStatus.Cancelled)
                .SumAsync(b => (int?)b.AttendeesCount, cancellationToken) ?? 0;

            if (existingAttendees + attendeesCount > capacityLimit)
                throw new InvalidOperationException("Venue capacity exceeded for this event");

            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                AttendeesCount = attendeesCount,
                Status = BookingStatus.Pending,
                BookingDate = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(cancellationToken);

            return booking;
        }

        public async Task<Booking> UpdateBookingStatusAsync(Guid bookingId, BookingStatus newStatus, CancellationToken cancellationToken)
        {
            if (bookingId == Guid.Empty) throw new ArgumentException("BookingId is required", nameof(bookingId));

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var booking = await _context.Bookings
                .Include(b => b.BookingItems)
                .Include(b => b.BookingPackages)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId, cancellationToken);

            if (booking == null) throw new KeyNotFoundException("Booking not found");

            var oldStatus = booking.Status;
            if (oldStatus == newStatus) return booking;

            var requiresDeduct = oldStatus != BookingStatus.Confirmed && newStatus == BookingStatus.Confirmed;
            var requiresRestore = oldStatus == BookingStatus.Confirmed && newStatus != BookingStatus.Confirmed;

            if (requiresDeduct)
            {
                await EnsureCapacityAsync(booking, cancellationToken);
                await AdjustInventoryAsync(booking, isRestore: false, cancellationToken);
            }
            else if (requiresRestore)
            {
                await AdjustInventoryAsync(booking, isRestore: true, cancellationToken);
            }

            booking.Status = newStatus;
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return booking;
        }

        public Task CancelBookingAsync(Guid bookingId, CancellationToken cancellationToken)
            => UpdateBookingStatusAsync(bookingId, BookingStatus.Cancelled, cancellationToken);

        private async Task EnsureCapacityAsync(Booking booking, CancellationToken cancellationToken)
        {
            var ev = await _context.Events
                .AsNoTracking()
                .Include(e => e.Venues)
                .FirstOrDefaultAsync(e => e.EventId == booking.EventId, cancellationToken);

            if (ev == null) throw new KeyNotFoundException("Event not found");
            if (ev.Venues == null || !ev.Venues.Any()) throw new KeyNotFoundException("Venue not found for the event");

            var totalVenueCapacity = ev.Venues.Sum(v => v.Capacity);
            var capacityLimit = Math.Min(ev.MaxAttendees, totalVenueCapacity);

            var otherAttendees = await _context.Bookings
                .AsNoTracking()
                .Where(b => b.EventId == booking.EventId && b.BookingId != booking.BookingId && b.Status != BookingStatus.Cancelled)
                .SumAsync(b => (int?)b.AttendeesCount, cancellationToken) ?? 0;

            if (otherAttendees + booking.AttendeesCount > capacityLimit)
                throw new InvalidOperationException("Venue capacity exceeded for this event");
        }

        private async Task AdjustInventoryAsync(Booking booking, bool isRestore, CancellationToken cancellationToken)
        {
            var requiredByItem = await CalculateRequiredItemQuantitiesAsync(booking, cancellationToken);
            if (requiredByItem.Count == 0) return;

            var itemIds = requiredByItem.Keys.ToList();

            var items = await _context.Items
                .Where(i => itemIds.Contains(i.ItemId))
                .ToListAsync(cancellationToken);

            if (items.Count != itemIds.Count)
                throw new KeyNotFoundException("One or more items referenced by booking were not found");

            var requiredByAsset = items
                .GroupBy(i => i.AssetId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(i => requiredByItem[i.ItemId])
                );

            var assetIds = requiredByAsset.Keys.ToList();
            var assets = await _context.Assets
                .Where(a => assetIds.Contains(a.AssetId))
                .ToListAsync(cancellationToken);

            if (assets.Count != assetIds.Count)
                throw new KeyNotFoundException("One or more assets referenced by booking items were not found");

            if (!isRestore)
            {
                foreach (var item in items)
                {
                    var required = requiredByItem[item.ItemId];
                    if (item.QuantityAvailable < required)
                        throw new InvalidOperationException($"Insufficient item quantity for itemId {item.ItemId}");
                }

                foreach (var asset in assets)
                {
                    var required = requiredByAsset[asset.AssetId];
                    if (asset.QuantityAvailable < required)
                        throw new InvalidOperationException($"Insufficient asset quantity for assetId {asset.AssetId}");
                }

                foreach (var item in items)
                {
                    var required = requiredByItem[item.ItemId];
                    item.QuantityAvailable -= required;
                }

                foreach (var asset in assets)
                {
                    var required = requiredByAsset[asset.AssetId];
                    asset.QuantityAvailable -= required;
                }

                return;
            }

            foreach (var item in items)
            {
                var required = requiredByItem[item.ItemId];
                item.QuantityAvailable += required;
            }

            foreach (var asset in assets)
            {
                var required = requiredByAsset[asset.AssetId];
                asset.QuantityAvailable += required;
            }
        }

        private async Task<Dictionary<Guid, int>> CalculateRequiredItemQuantitiesAsync(Booking booking, CancellationToken cancellationToken)
        {
            var required = new Dictionary<Guid, int>();

            foreach (var bi in booking.BookingItems)
            {
                if (bi.ItemId == Guid.Empty) continue;
                if (bi.Quantity <= 0) continue;

                required[bi.ItemId] = required.TryGetValue(bi.ItemId, out var existing)
                    ? existing + bi.Quantity
                    : bi.Quantity;
            }

            var packageIds = booking.BookingPackages
                .Select(bp => bp.PackageId)
                .Where(id => id != Guid.Empty)
                .Distinct()
                .ToList();

            if (packageIds.Count == 0) return required;

            var packageItems = await _context.PackageItems
                .AsNoTracking()
                .Where(pi => packageIds.Contains(pi.PackageId))
                .ToListAsync(cancellationToken);

            foreach (var pi in packageItems)
            {
                if (pi.ItemId == Guid.Empty) continue;
                if (pi.Quantity <= 0) continue;

                required[pi.ItemId] = required.TryGetValue(pi.ItemId, out var existing)
                    ? existing + pi.Quantity
                    : pi.Quantity;
            }

            return required;
        }
    }
}
