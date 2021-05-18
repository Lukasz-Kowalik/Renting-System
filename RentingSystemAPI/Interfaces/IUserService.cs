using Microsoft.AspNetCore.Identity;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Models.Requests;
using RentingSystemAPI.Models.Responses;
using System.Collections.Generic;
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

        Task<User> GetUser(string email);

        Task<string> Refresh(User user);

        Task<User> GetUserAsync(ClaimsPrincipal userPrincipal, string email = null);

        int GetUserId(ClaimsPrincipal userPrincipal, string email = null);

        Task SeedRolesAsync();

        Task<bool> ResetUserPassword(ResetPasswordRequest request);

        Task ChangeUserRole(int userId, int roleId);

        Task<IEnumerable<AdminPanelResponse>> GetUserAdminList();
        Task ChangeMaxDays(int userId, int days);
    }
}