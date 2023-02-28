﻿using AutoMapper;
using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;

namespace DeskBookingAPI
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Desk, RoomDeskDto>();
            CreateMap<Desk, DeskDto>();
        }
    }
}
