using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystemAPI.Interfaces
{
    public interface IRentService
    {
        Task<bool> Add(ClaimsPrincipal userPrincipal, string userEmail = null);
    }
}