using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace RentingSystemAPI.Commands
{
    public class CreateUserCommand: IRequest<IdentityResult>
    {
        public Object Json { get;} 

        public CreateUserCommand(Object JSON)
        {
            Json = JSON;
        }
    }
}
