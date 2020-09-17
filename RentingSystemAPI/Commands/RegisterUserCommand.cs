using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RentingSystemAPI.DTOs.Request;

namespace RentingSystemAPI.Commands
{
    public class RegisterUserCommand : IRequest<IdentityResult>
    {
        public RegisterUserRequest User { get; }

        public RegisterUserCommand(RegisterUserRequest user)
        {
            User = user;
        }
    }
}