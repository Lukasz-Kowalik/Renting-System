using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RentingSystem.Models;
using RentingSystem.ViewModels.Authorization;
using RentingSystem.ViewModels.DTOs;
using RentingSystem.ViewModels.Models;

namespace RentingSystem.Mapping
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserVm,UserDto>();

            //user registration 
            CreateMap<UserDto, PasswordHasher>();
            CreateMap<UserDto, RegisteredUser>();
            CreateMap<PasswordHasher, RegisteredUser>();
        }
    }
}
