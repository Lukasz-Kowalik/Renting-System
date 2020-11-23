using Microsoft.AspNetCore.Identity;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.Models.Requests;
using RentingSystemAPI.Models.Responses;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystemAPI.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> LoginAsync(AuthenticateRequest request);

        Task<IdentityResult> RegisterAsync(RegisterUserRequest userRequest);

        User GetById(int id);

        Task LogoutAsync();

        Task<string> Refresh(User user);

        Task<User> GetUserAsync(ClaimsPrincipal userPrincipal, string email = null);

        int GetUserId(ClaimsPrincipal userPrincipal, string email = null);

        Task SeedRolesAsync();
    }
}