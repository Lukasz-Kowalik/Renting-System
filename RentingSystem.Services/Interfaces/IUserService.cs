using RentingSystem.ViewModels.Models;
using System.Net.Http;
using System.Threading.Tasks;
using RentingSystem.ViewModels.DTOs;

namespace RentingSystem.Services.Interfaces
{
    public interface IUserService
    {
         Task<HttpResponseMessage> RegisterAsync(UserDto userDto, HttpClient client);
    }
}