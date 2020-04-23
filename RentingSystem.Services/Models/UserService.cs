using RentingSystem.Requests;
using RentingSystem.Services.Interfaces;
using RentingSystem.ViewModels.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Services.Models
{
    public class UserService : IUserService
    {
        public async Task<HttpResponseMessage> RegisterAsync(UserVm userVm, HttpClient client)
        {
            var response = await client.SendPostAsync("CreateUser", userVm);

            return response;
        }
    }
}