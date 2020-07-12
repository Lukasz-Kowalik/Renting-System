using MediatR;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentingSystemAPI.Handlers.Queries
{
    public class GatAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly RentingContext _context;

        public GatAllUsersHandler(RentingContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _context.Users.OfType<User>().AsNoTracking().ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}