using AutoMapper;
using RentingSystem.Services.Interfaces;
using RentingSystem.Services.Requests;
using RentingSystem.ViewModels.DTOs;
using RentingSystem.ViewModels.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public async Task<HttpResponseMessage> RegisterAsync(UserVm userVm, HttpClient client)
        {
            try
            {
                var passwordDto = _mapper.Map<PasswordDto>(userVm);
                var userDto = _mapper.Map<UserDto>(userVm);
                _mapper.Map<PasswordDto, UserDto>(passwordDto, userDto);

                var response = await client.SendPostAsync("/CreateUser", userDto);

                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}