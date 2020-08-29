using MediatR;
using System;
using RentingSystemAPI.Models.Requests;
using RentingSystemAPI.Models.Responses;

namespace RentingSystemAPI.Commands
{
    public class LoginCommand : IRequest<AuthenticateResponse>
    {
        public AuthenticateRequest Json { get; }

        public LoginCommand(AuthenticateRequest json)
        {
            Json = json;
        }
    }
}