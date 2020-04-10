using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentingSystem.Models;
using RentingSystem.Models.Accounts;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(ILogger<AccountController> logger, IHttpClientFactory httpClientFactory)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            _logger = logger;
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
        public async Task<IActionResult> RegisterAsync([FromForm] RegisteredUser user)
        {
            //
            //https://github.com/dotnet/runtime/issues/20682
            //
            var client = _httpClientFactory.CreateClient("API Client");

            try
            {
                var response = await client.GetAsync("/Users");
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return RedirectToAction("Index", "Info");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

            // //  var response = await client.SendPostAsync("http://rentingsystemapi:5000/CreateUser", user);
            ////   to do check if pass get method
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