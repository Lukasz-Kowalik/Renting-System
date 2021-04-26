using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystemAPI.Interfaces
{
    public interface IRentedItemsService
    {
        IEnumerable<RentedItemsResponse> Get(ClaimsPrincipal userPrincipal, string userEmail = null);

        IEnumerable<RentedItemsResponseWithUsers> GetAll();

        Task<bool> ReturnItems(ClaimsPrincipal userPrincipal, ReturnItemsRequest request);
    }
}