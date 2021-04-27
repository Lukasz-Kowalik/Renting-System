using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentingSystem.Models;
using RentingSystem.ViewModels.Vms;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Items/{id}")]
        public async Task<IActionResult> ItemAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("API Client");
            var response = await client.GetAsync($"Items/{id}");
            response.EnsureSuccessStatusCode();

            if (response.Content is object && response.Content.Headers.ContentType.MediaType == "application/json")
            {
                var contentStream = await response.Content.ReadAsStreamAsync();

                try
                {
                    var result = await System.Text
                        .Json.JsonSerializer
                        .DeserializeAsync<ItemModel>
                        (contentStream, new System.Text.Json.JsonSerializerOptions
                        { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
                    return View(result);
                }
                catch (Exception)
                {
                    return Error();
                }
            }

            return Error();
        }
    }
}