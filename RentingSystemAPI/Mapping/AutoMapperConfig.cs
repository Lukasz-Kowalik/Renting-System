using System.Collections.Generic;
using AutoMapper;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;

namespace RentingSystemAPI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RegisterUserRequest, User>()
                .ForMember(u => u.UserName, m => m.MapFrom(u => u.Email))
                .ReverseMap();
            CreateMap<Item, ItemListResponse>();
        }
    }
}