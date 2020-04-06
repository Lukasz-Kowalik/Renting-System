using DAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.Model;
using RentingSystemAPI.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace RentingSystemAPI.Handlers.Queries
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly RentingContext _context;

        public GetUserByIdHandler(RentingContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(t => t.Id == request.UserId);
        }
    }
}