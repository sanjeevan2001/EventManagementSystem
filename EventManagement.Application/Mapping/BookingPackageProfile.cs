using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;

namespace EventManagement.Application.Mapping
{
    public class BookingPackageProfile : Profile
    {
        public BookingPackageProfile()
        {
            CreateMap<BookingPackage, BookingPackageDto>().ReverseMap();
        }
    }
}
