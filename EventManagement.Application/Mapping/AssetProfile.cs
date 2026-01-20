using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;

namespace EventManagement.Application.Mapping
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<Asset, AssetDto>().ReverseMap();
        }
    }
}
