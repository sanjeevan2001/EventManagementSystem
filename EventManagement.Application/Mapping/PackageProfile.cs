using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Mapping
{
    public class PackageProfile :Profile
    {
        public PackageProfile()
        {
            CreateMap<Package, PackageDto>().ReverseMap();
        }
    }
}
