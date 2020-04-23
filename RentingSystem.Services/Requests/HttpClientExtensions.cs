using Newtonsoft.Json;
using RentingSystem.ViewModels.Models;
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
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            HttpResponseMessage response = null;
            try
            {
                //var URI = new Uri(URL);
                var json = JsonConvert.SerializeObject(userVm);

                using (var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"))
                {
                    // response = await client.PostAsync(URI, content);
                    response = await client.PostAsync(URL, content);
                }

                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Debug.WriteLine(elapsedTime);
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