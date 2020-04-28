using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RentingSystem.ViewModels.DTOs;
using RentingSystem.ViewModels.Models;

namespace RentingSystem.Mapping
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserVm,UserDto>();
            CreateMap<UserVm, PasswordDto>();
            CreateMap<PasswordDto, UserDto>();
        }
    }
}
