using AutoMapper;
using Hasher.Wrappers;
using RentingSystem.ViewModels.Authorization;
using RentingSystem.ViewModels.DTOs;
using RentingSystem.ViewModels.Vms;

namespace RentingSystem.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserVm, UserDto>();

            //user registration
            CreateMap<UserDto, PasswordHasherWrapper>();
            CreateMap<UserDto, RegisteredUser>();
            CreateMap<PasswordHasherWrapper, RegisteredUser>();

            //user logging
            CreateMap<LoginDto, LoggedUser>();
            CreateMap<LoginDto, PasswordHasherWrapper>();
            CreateMap<PasswordHasherWrapper, LoggedUser>();
        }
    }
}