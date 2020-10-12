using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentingSystem.Models;
using RentingSystem.Services.Interfaces;
using RentingSystem.ViewModels.DTOs;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;

        public AccountController(IUserService userService, IHttpClientFactory httpClientFactory, IMapper mapper, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, AppDbContext context)
        {
            _userService = userService;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
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

            try
            {
                var client = _httpClientFactory.CreateClient("API Client");
                var response = await _userService.LoginAsync(userDto, client);

                if (!response.IsSuccessStatusCode) throw new Exception("");

                var responseContent = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(responseContent);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(userResponse.Token);
                var user = _mapper.Map<IdentityUser>(userResponse);
                var role = token.Claims.FirstOrDefault(x => x.Type.Contains("role"))?.Value;

                if (role == null) throw new Exception("User doesn't have role");
                //await _context.Roles.AddAsync(new IdentityRole(role));
                // await _roleManager.CreateAsync(new IdentityRole(role));
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Email, user.Email),
                //};
                //claims.AddRange(token.Claims);

                var result = await _userManager.CreateAsync(user, userDto.Password);
                //if (!result.Succeeded)
                //{
                //}
                //  if (!result.Succeeded) throw new Exception("");

                //await _userManager.AddClaimsAsync(user, token.Claims);
                ////try
                ////{
                //await _userManager.AddToRoleAsync(user, role);

                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e);
                //    throw;
                //}
                //var userIdentity = new ClaimsIdentity(claims, "userClaims");
                //var tokenIdentity = new ClaimsIdentity(token.Claims, "token");
                //var userPrincipal = new ClaimsPrincipal(new[] { userIdentity, tokenIdentity });
                //await HttpContext.SignInAsync(userPrincipal);

                //  await _signInManager.SignInAsync(user, false);
                var returnUrl = (string)TempData["returnUrl"];

                if (returnUrl != null)
                {
                    return RedirectToPage(returnUrl);
                }

                return RedirectToAction("Index", "Home");

                //// await _signInManager.SignInAsync(user, false);
                //// await _signInManager.SignInAsync(user)

                //var userIdentity = new ClaimsIdentity(claims, "userClaims");
                //var tokenIdentity = new ClaimsIdentity(token.Claims, "token");
                //var userPrincipal = new ClaimsPrincipal(new[] { userIdentity, tokenIdentity });

                //  await HttpContext.SignInAsync(userRole?.Value, userPrincipal);

                //  await HttpContext.SignInAsync(userPrincipal);
            }
            catch (Exception e)
            {
                ViewData["Response"] = "Something goes wrong!";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync();
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
                var user = _mapper.Map<IdentityUser>(userDto);
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