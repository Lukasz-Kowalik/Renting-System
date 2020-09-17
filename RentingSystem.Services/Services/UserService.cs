using AutoMapper;
using RentingSystem.Services.Interfaces;
using RentingSystem.Services.Requests;
using RentingSystem.ViewModels.Authorization;
using RentingSystem.ViewModels.DTOs;
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

        public async Task<HttpResponseMessage> RegisterAsync(UserDto userDto, HttpClient client)
        {
            try
            {
                var registeredUser = _mapper.Map<RegisteredUser>(userDto);

                var response = await client.PostAsJsonAsync("/RegisterUser", registeredUser);

                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginDto userDto, HttpClient client)
        {
            try
            {
                var response = await client.PostAsJsonAsync("/Login", userDto);

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