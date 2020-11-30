using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;
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
            UserManager<User> userManager, IMapper mapper,
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

        public async Task SeedRolesAsync()
        {
            var users = _context.Users.ToArray();

            await _userManager.AddToRoleAsync(users[0], nameof(AccountTypes.User));
            await _userManager.AddClaimAsync(users[0], new Claim(ClaimTypes.Role, nameof(AccountTypes.User)));

            await _userManager.AddToRoleAsync(users[1], nameof(AccountTypes.Customer));
            await _userManager.AddClaimAsync(users[1], new Claim(ClaimTypes.Role, nameof(AccountTypes.Customer)));

            await _userManager.AddToRoleAsync(users[2], nameof(AccountTypes.Worker));
            await _userManager.AddClaimAsync(users[2], new Claim(ClaimTypes.Role, nameof(AccountTypes.Worker)));

            await _userManager.AddToRoleAsync(users[3], nameof(AccountTypes.Admin));
            await _userManager.AddClaimAsync(users[3], new Claim(ClaimTypes.Role, nameof(AccountTypes.Admin)));

            await _userManager.RemoveFromRoleAsync(users[4], nameof(AccountTypes.User));
            await _userManager.RemoveClaimAsync(users[4], new Claim(ClaimTypes.Role, nameof(AccountTypes.User)));

            await _userManager.AddToRoleAsync(users[4], nameof(AccountTypes.Admin));
            await _userManager.AddClaimAsync(users[4], new Claim(ClaimTypes.Role, nameof(AccountTypes.Admin)));
        }

        public async Task<bool> ResetUserPassword(ResetPasswordRequest request)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == request.Email);
            if (user == null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(user, request.Password1);
        }

        public async Task ChangeUserRole(int userId, int roleId)
        {
            var user = GetById(userId);
            string oldUserRole = await GetUserRole(user);
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _userManager.RemoveFromRoleAsync(user, oldUserRole);
                await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, oldUserRole));
                await _context.SaveChangesAsync();

                var accoutntType = ((AccountTypes)roleId).ToString();
                await _userManager.AddToRoleAsync(user, accoutntType);
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, accoutntType));
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<string> GetUserRole(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.First();
        }

        public async Task<IEnumerable<AdminPanelResponse>> GetUserAdminList()
        {
            var users = _context.Users.ToArray();

            var result = _mapper.Map<AdminPanelResponse[]>(users);
            for (int i = 0; i < users.Length; i++)
            {
                var role = await GetUserRole(users[i]);
                result[i].Role = (AccountTypes)Enum.Parse(typeof(AccountTypes), role);
            }
            return result;
        }
    }
}