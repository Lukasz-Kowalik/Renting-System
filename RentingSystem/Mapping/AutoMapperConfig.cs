using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RentingSystem.Models;
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

            CreateMap<UserDto, RegisteredUser>();

            //user logging
            CreateMap<LoginDto, LoggedUser>();
        }
    }
}