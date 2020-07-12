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
                var passwordHasher = _mapper.Map<PasswordHasher>(userDto);
                var registeredUser = _mapper.Map<RegisteredUser>(userDto);
                _mapper.Map<PasswordHasher, RegisteredUser>(passwordHasher, registeredUser);

                var response = await client.PostAsJsonAsync("/CreateUser", registeredUser);

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