using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Commands;
using RentingSystemAPI.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentingSystemAPI.Handlers.Commands
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, Object>
    {
        private readonly UserManager<User> _userManager;

        //private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        private readonly IConfiguration _config;

        public LoginUserHandler(UserManager<User> userManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
        }

        public async Task<object> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = JsonConvert.DeserializeObject<UserDto>(request.Json.ToString() ?? string.Empty);
            var user = _userManager.Users.Where(x => x.Email == userDto.Email).ToList();
            if (!user.Any())
            {
                throw new ArgumentNullException("user doesn't exist");
            }
            //map entity to dto pass too gen
            //ChangePasswordAsync  if true ret
            //   return GenerateJSONWebToken(user[0]);
            //json =>objekt
            return null;
        }
    }
}