using AutoMapper;
using EventManagement.Application.Abstraction.Persistences.IRepositories;
using EventManagement.Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.bookingpackage.Queries.GetBookingPackageById
{
    public class getBookingPackageByIdHandler : IRequestHandler<GetBookingPackageByIdQuery, BookingPackageDto?>
    {
        private readonly IBookingPackageRepository _repo;
        private readonly IMapper _mapper;

        public getBookingPackageByIdHandler(IBookingPackageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<BookingPackageDto?> Handle(GetBookingPackageByIdQuery request, CancellationToken cancellationToken)
        {
            var link = await _repo.GetByIdAsync(request.BookingId, request.PackageId);
            return link == null ? null : _mapper.Map<BookingPackageDto>(link);
        }
    }
}
