﻿using Microsoft.AspNetCore.Identity;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.Models.Requests;
using RentingSystemAPI.Models.Responses;
using System.Threading.Tasks;

namespace RentingSystemAPI.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);

        Task<IdentityResult> RegisterAsync(RegisterUserRequest userRequest);

        User GetById(int id);
    }
}