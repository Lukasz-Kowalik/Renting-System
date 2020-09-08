using AutoMapper;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs;
using RentingSystemAPI.DTOs.Request;

namespace RentingSystemAPI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RegisterUserRequest, User>()
                .ForMember(u => u.UserName, m => m.MapFrom(u => u.Email))
                .ReverseMap();
        }
    }
}