using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;

namespace EventManagement.Application.Mapping
{
    public class BookingItemProfile : Profile
    {
        public BookingItemProfile()
        {
            CreateMap<BookingItem, BookingItemDto>()
                .ForMember(d => d.ItemName, opt => opt.MapFrom(s => s.Item != null ? s.Item.Name : null))
                .ForMember(d => d.ItemType, opt => opt.MapFrom(s => s.Item != null ? s.Item.Type : null))
                .ForMember(d => d.ItemPrice, opt => opt.MapFrom(s => s.Item != null ? (decimal?)s.Item.Price : null))
                .ReverseMap();
        }
    }
}
