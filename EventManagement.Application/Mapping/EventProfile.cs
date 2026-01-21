using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>()
                .ForMember(d => d.Venues, o => o.MapFrom(s => s.Venues))
                .ReverseMap();
        }
    }
}
