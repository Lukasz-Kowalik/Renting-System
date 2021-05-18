using RentingSystemAPI.BAL.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystemAPI.Interfaces
{
    public interface IRentService
    {
        Task<bool> Add(ClaimsPrincipal userPrincipal, string userEmail = null);

        Rent GetRent(int id);

        Task<bool> Add(string userEmail);
    }
}