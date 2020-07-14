using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentingSystem.Models;
using RentingSystem.Services.Interfaces;
using RentingSystem.ViewModels.DTOs;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(ILogger<AccountController> logger, IUserService userService, IHttpClientFactory httpClientFactory)
        {
            //  ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            _logger = logger;
            _userService = userService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
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