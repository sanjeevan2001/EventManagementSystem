using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;

namespace EventManagement.Application.Mapping
{
    public class PackageItemProfile : Profile
    {
        public PackageItemProfile()
        {
            CreateMap<PackageItem, PackageItemDto>().ReverseMap();
        }
    }
}
