using MediatR;
using RentingSystemAPI.BAL.Entities;
using System.Collections.Generic;

namespace RentingSystemAPI.Queries
{
    public class GetAllRentsByUserIdQuery : IRequest<List<Rent>>
    {
        public int UserId { get; }

        public GetAllRentsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}