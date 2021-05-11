using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentingSystem.Models;
using RentingSystem.Services.Interfaces;
using RentingSystem.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public AccountController(IUserService userService, IHttpClientFactory httpClientFactory, IMapper mapper
            , SignInManager<User> signInManager, UserManager<User> userManager, AppDbContext context)
        {
            _userService = userService;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            CreateRoles();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var client = _httpClientFactory.CreateClient("API Client");
            var response = await _userService.LoginAsync(userDto, client);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(responseContent);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(userResponse.Token);
                var user = _mapper.Map<User>(userResponse);
                user.Id = Int32.Parse(token.Claims.FirstOrDefault(x => x.Type.Contains("nameid"))?.Value ?? string.Empty);

                var result = await _userManager.CreateAsync(user, userDto.Password);
                var returnUrl = (string)TempData["returnUrl"];
                if (result.Succeeded)
                {
                    var claims = new List<Claim>()
                {
                    new Claim("token", token.ToString())
                };
                    claims.AddRange(token.Claims);
                    await _userManager.AddClaimsAsync(user, claims);
                    var userRole = token.Claims.FirstOrDefault(x => x.Type.Contains("role"))?.Value ?? string.Empty;

                    await _userManager.AddToRoleAsync(user, userRole);

                    var singInResult = await _signInManager.PasswordSignInAsync(user, userDto.Password, false, false);

                    if (singInResult.Succeeded)
                    {
                        if (returnUrl == null)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        return RedirectToPage(returnUrl);
                    }
                }
                else
                {
                    //user already exists in database
                    return RedirectToPage(returnUrl);
                }
            }

            ViewData["Response"] = "Something goes wrong!";

            return View();
        }

        private void CreateRoles()
        {
            if (_context.Roles.Count() <= 0)
            {
                var roles = new List<Role>
            {
                new Role {Id = 1,Name = nameof(AccountTypes.User),NormalizedName  = nameof(AccountTypes.User).ToUpper()},
                new Role {Id = 2,Name = nameof(AccountTypes.Admin),NormalizedName  = nameof(AccountTypes.Admin).ToUpper()},
            };
                _context.Roles.AddRange(roles);
                _context.SaveChanges();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                await _userManager.RemoveClaimsAsync(user, claims);
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                var client = _httpClientFactory.CreateClient("API Client");
                await _userService.LogOutAsync(client);
                await _userManager.DeleteAsync(user);
            }

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var client = _httpClientFactory.CreateClient("API Client");
            var response = await _userService.RegisterAsync(userDto, client);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Account");
            }

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                ViewData["ResponseMessage"] = "Email is unavailable";
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                await _userManager.RemoveClaimsAsync(user, claims);
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.DeleteAsync(user);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}