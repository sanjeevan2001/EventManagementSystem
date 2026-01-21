using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Mapping
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.EventName, opt => opt.MapFrom(s => s.Event != null ? s.Event.Name : null))
                .ForMember(d => d.EventStartDate, opt => opt.MapFrom(s => s.Event != null ? (DateTime?)s.Event.StartDate : null))
                .ForMember(d => d.EventEndDate, opt => opt.MapFrom(s => s.Event != null ? (DateTime?)s.Event.EndDate : null));
        }
    }
}
