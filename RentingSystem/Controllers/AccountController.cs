using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentingSystem.Models;
using RentingSystem.Services.Interfaces;
using RentingSystem.ViewModels.DTOs;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _userService = userService;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
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
                var user = JsonConvert.DeserializeObject<AuthenticateResponse>(responseContent);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(user.Token);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                };

                var userIdentity = new ClaimsIdentity(claims, "user");
                var tokenIdentity = new ClaimsIdentity(token.Claims, "token");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity, tokenIdentity });

                await HttpContext.SignInAsync(userPrincipal);

                var returnUrl = (string)TempData["returnUrl"];

                if (returnUrl != null)
                {
                    return RedirectToPage(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

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
                return RedirectToAction("Index", "Home");
            }

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                ViewData["ResponseMessage"] = "Email is unavailable";
            }

            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}