using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;

namespace EventManagement.Application.Mapping
{
    public class BookingPackageProfile : Profile
    {
        public BookingPackageProfile()
        {
            CreateMap<BookingPackage, BookingPackageDto>()
                .ForMember(d => d.PackageName, o => o.MapFrom(s => s.Package != null ? s.Package.Name : string.Empty))
                .ForMember(d => d.Assets, o => o.MapFrom(s => s.Package != null ? s.Package.Assets : new List<Asset>()))
                .ReverseMap();
        }
    }
}
