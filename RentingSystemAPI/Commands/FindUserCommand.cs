using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace RentingSystemAPI.Commands
{
    public class FindUserCommand:IRequest<Object>
    {
        public Object Json { get; }

        public FindUserCommand(Object JSON)
        {
            Json = JSON;
        }
    }
}
