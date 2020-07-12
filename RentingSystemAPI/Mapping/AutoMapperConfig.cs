using AutoMapper;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs;

namespace RentingSystemAPI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserDto, User>()
                .ForMember(u => u.UserName, m => m.MapFrom(u => u.Email));
        }
    }
}