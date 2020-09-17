using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RentingSystem.Services.Helpers
{
    public static class ContentFromHttpResponseMessage
    {
        public static async Task<string> Get(HttpResponseMessage response)
        {
            Stream receiveStream = await response.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var content = await readStream.ReadToEndAsync();
            return content;
        }
    }
}