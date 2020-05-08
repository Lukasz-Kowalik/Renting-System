using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RentingSystem.ViewModels.DTOs;

namespace RentingSystem.Services.Requests
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> SendPostAsync(this HttpClient client, string URL, object objectToSerialize)
        {
            HttpResponseMessage response = null;
            try
            {
                var json = JsonConvert.SerializeObject(objectToSerialize);

                using (var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"))
                {
                    response = await client.PostAsync(URL, content);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return response;
        }
    }
}