using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RentingSystemAPI.Handlers.Queries
{
    public class GetAllUserRentsHandler:IRequestHandler<GetAllRentsByUserIdQuery, List<Rent>>
    {
        private readonly RentingContext _context;

        public GetAllUserRentsHandler(RentingContext context)
        {
            _context = context;
        }

        public async Task<List<Rent>> Handle(GetAllRentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return  _context.Rents.Where(x => x.UserId == request.UserId).ToList();
        }
    }
}