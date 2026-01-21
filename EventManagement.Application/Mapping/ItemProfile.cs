using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Mapping
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>()
                .ForMember(d => d.AssetName, o => o.MapFrom(s => s.Asset != null ? s.Asset.Name : string.Empty))
                .ForMember(d => d.PackageName, o => o.MapFrom(s => s.Asset != null && s.Asset.Package != null ? s.Asset.Package.Name : string.Empty))
                .ReverseMap();
        }
    }
}
