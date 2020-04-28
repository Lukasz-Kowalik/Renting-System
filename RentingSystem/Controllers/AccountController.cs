using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentingSystem.Models;
using RentingSystem.Services.Interfaces;
using RentingSystem.ViewModels.Models;
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
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserVm userVm)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", userVm);
            }
            var client = _httpClientFactory.CreateClient("API Client");
            var response = await _userService.RegisterAsync(userVm,client);
           
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return RedirectToAction("Index", "Info");
            }

            if (response.IsSuccessStatusCode)
            {
                //  var content = await response.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "Home");
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