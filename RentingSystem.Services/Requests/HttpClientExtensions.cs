using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Requests
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> SendPostAsync(this HttpClient client, string URL, object userVm)
        {
            HttpResponseMessage response = null;
            try
            {
                var json = JsonConvert.SerializeObject(userVm);

                using (var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"))
                {
                    // response = await client.PostAsync(URI, content);
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