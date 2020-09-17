using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Services.Requests
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string url, object objectToSerialize)
        {
            HttpResponseMessage response = null;
            try
            {
                var json = JsonConvert.SerializeObject(objectToSerialize, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await client.PostAsync(url, content);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }

        public static async Task<object> GetJsonAsync(this HttpClient client, string url, Type objectType)
        {
            dynamic entity;
            using (Stream s = await client.GetStreamAsync(url))
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer();
                entity = serializer.Deserialize<ObjectType>(reader);
                return entity;
            }
        }
    }
}