using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace RentingSystemAPI.Commands
{
    public class LoginUserCommand:IRequest<Object>
    {
        public Object Json { get; }

        public LoginUserCommand(Object JSON)
        {
            Json = JSON;
        }
    }
}
