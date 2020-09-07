using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs;
using RentingSystemAPI.Helpers;
using RentingSystemAPI.Interfaces;
using RentingSystemAPI.Models.Requests;
using RentingSystemAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentingSystemAPI.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RentingContext _context;
        private readonly AppSettings _appSettings;

        public UserService(SignInManager<User> signInManager,
            UserManager<User> userManager,
            IMapper mapper,
            IOptions<AppSettings> appSettings, RentingContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest Json)
        {
            //  var userDto = JsonConvert.DeserializeObject<UserDto>(Json.ToString() ?? string.Empty);

            var user = await _userManager.FindByEmailAsync(Json.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, Json.Password, false);
            if (result.Succeeded)
            {
                // authentication successful so generate jwt token
                var token = generateJwtToken(user);

                return new AuthenticateResponse(user, token);
            }
            else
            {
                throw new System.Exception("Incorrect user");
            }
        }

        public async Task<IdentityResult> RegisterAsync(Object Json)
        {
            try
            {
                var userDto = JsonConvert.DeserializeObject<UserDto>(Json.ToString() ?? string.Empty);
                var user = _mapper.Map<User>(userDto);
                var claims = new List<Claim>();
               // claims.Add(new Claim(ClaimTypes.Email, user.Email));
              //  claims.Add(new Claim(ClaimTypes.Role, nameof(AccountTypes.User)));
                var result = await _userManager.CreateAsync(user, userDto.Password);
                //await _userManager.AddClaimsAsync(user, claims);
                //await _userManager.AddToRoleAsync(user, nameof(AccountTypes.User));
                return result;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}