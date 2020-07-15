using AutoMapper;
using RentingSystem.ViewModels.Authorization;
using RentingSystem.ViewModels.DTOs;
using RentingSystem.ViewModels.Models;

namespace RentingSystem.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserVm, UserDto>();

            //user registration
            CreateMap<UserDto, PasswordHasher>();
            CreateMap<UserDto, RegisteredUser>();
            CreateMap<PasswordHasher, RegisteredUser>();

            //user logging
            CreateMap<LoginDto, LoggedUser>();
            CreateMap<LoginDto, PasswordHasher>();
            CreateMap<PasswordHasher, LoggedUser>();
        }
    }
}