using RentingSystem.ViewModels.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Services.Interfaces
{
    public interface IUserService
    {
         Task<HttpResponseMessage> RegisterAsync(UserVm userVm, HttpClient client);
    }
}