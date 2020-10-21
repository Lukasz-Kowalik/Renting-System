using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Request;
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

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                var token = await generateJwtToken(user);

                return new AuthenticateResponse(user, token);
            }
            else
            {
                throw new Exception("Incorrect user");
            }
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUserRequest userRequest)
        {
            try
            {
                var user = _mapper.Map<User>(userRequest);
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, nameof(AccountTypes.User)));
                var result = await _userManager.CreateAsync(user, userRequest.Password);
                await _userManager.AddClaimsAsync(user, claims);
                await _userManager.AddToRoleAsync(user, nameof(AccountTypes.User));
                return result;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private async Task<string> generateJwtToken(User user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            // generate token that is valid for 1 day
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Claims = new JwtPayload(claims)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public async Task<string> Refresh(User user)
        {
            return await generateJwtToken(user);
        }
    }
}