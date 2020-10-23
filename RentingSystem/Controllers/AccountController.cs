using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentingSystem.Models;
using RentingSystem.Services.Interfaces;
using RentingSystem.ViewModels.DTOs;
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
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(IUserService userService, IHttpClientFactory httpClientFactory, IMapper mapper
            , SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userService = userService;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
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
                user.Id = token.Claims.FirstOrDefault(x => x.Type.Contains("nameid"))?.Value;

                var claims = new List<Claim>()
                {
                    new Claim("token", token.ToString())
                };
                claims.AddRange(token.Claims);

                var result = await _userManager.CreateAsync(user, userDto.Password);
                var returnUrl = (string)TempData["returnUrl"];
                if (result.Succeeded)
                {
                    await _userManager.AddClaimsAsync(user, claims);

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

        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
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
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(user, userDto.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
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