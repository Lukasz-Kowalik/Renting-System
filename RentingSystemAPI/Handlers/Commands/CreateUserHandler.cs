using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentingSystemAPI.Commands;
using RentingSystemAPI.DAL.Context;

namespace RentingSystemAPI.Handlers.Commands
{
    public class CreateUserHandler:IRequestHandler<CreateUserCommand,Object>
    {
        private readonly RentingContext _context;
        public CreateUserHandler(RentingContext context)
        {
            _context = context;
        }
        public async Task<Object> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
