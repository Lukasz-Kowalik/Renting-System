using Microsoft.AspNetCore.Mvc;
using RentingSystem.Models;
using RentingSystem.ViewModels.Vms;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ItemController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Items/{id}")]
        public async Task<IActionResult> Index(int id)
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
                    ViewData["ItemId"] = id;
                    return View(result);
                }
                catch (Exception)
                {
                    return Error();
                }
            }

            return Error();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}