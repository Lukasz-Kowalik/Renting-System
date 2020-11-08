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

        public async Task<AuthenticateResponse> LoginAsync(AuthenticateRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(request.Email, request.Password, true, false);
                var token = await GenerateJwtToken(user);

                return new AuthenticateResponse(user, token);
            }

            throw new Exception("Incorrect user");
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUserRequest userRequest)
        {
            try
            {
                var user = _mapper.Map<User>(userRequest);
                var result = await _userManager.CreateAsync(user, userRequest.Password);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, nameof(AccountTypes.User))
                };
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

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            // generate token that is valid for 1 day
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
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
            return await GenerateJwtToken(user);
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal userPrincipal, string email = null)
        {
            return await _userManager.GetUserAsync(userPrincipal) ?? _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public int GetUserId(ClaimsPrincipal userPrincipal, string email = null)
        {
            var id = _userManager.GetUserId(userPrincipal);
            if (id == null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    throw new NullReferenceException("User doesn't exist");
                }
                return user.Id;
            }
            else
            {
                return Int32.Parse(id);
            }
        }
    }
}