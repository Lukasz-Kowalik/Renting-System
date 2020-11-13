using System.Security.Claims;
using System.Threading.Tasks;
using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.Interfaces
{
    public interface IRentService
    {
        Task<bool> Add(ClaimsPrincipal userPrincipal, string userEmail = null);

        Rent GetRent(int id);
    }
}