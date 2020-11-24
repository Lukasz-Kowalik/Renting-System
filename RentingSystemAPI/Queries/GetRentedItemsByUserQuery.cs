using MediatR;
using RentingSystemAPI.BAL.Entities;
using System.Collections.Generic;

namespace RentingSystemAPI.Queries
{
    public class GetRentedItemsByUserQuery : IRequest<List<RentedItem>>
    {
        private readonly int? _userId;

        public GetRentedItemsByUserQuery(int? userId)
        {
            _userId = userId;
        }
    }
}