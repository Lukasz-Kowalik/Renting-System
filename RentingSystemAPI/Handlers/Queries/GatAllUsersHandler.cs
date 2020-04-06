using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.Controllers;
using RentingSystemAPI.Model;

namespace RentingSystemAPI.Handlers.Queries
{
    public class GatAllUsersHandler:IRequestHandler<GetAllUsersQuery,List<User>>
    {
        private readonly RentingContext _context;
       

        public GatAllUsersHandler(RentingContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Users.ToListAsync();
            return result;
        }
    }
}
