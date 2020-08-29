using Microsoft.AspNetCore.Identity;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Models.Requests;
using RentingSystemAPI.Models.Responses;
using System;
using System.Threading.Tasks;

namespace RentingSystemAPI.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest Json);

        Task<IdentityResult> RegisterAsync(Object Json);

        User GetById(int id);
    }
}