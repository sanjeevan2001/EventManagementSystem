using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;

namespace EventManagement.Application.Mapping
{
    public class BookingItemProfile : Profile
    {
        public BookingItemProfile()
        {
            CreateMap<BookingItem, BookingItemDto>().ReverseMap();
        }
    }
}
