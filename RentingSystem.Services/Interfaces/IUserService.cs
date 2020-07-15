using RentingSystem.ViewModels.DTOs;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<HttpResponseMessage> RegisterAsync(UserDto userDto, HttpClient client);

        Task<HttpResponseMessage> LoginAsync(LoginDto userDto, HttpClient client);
    }
}