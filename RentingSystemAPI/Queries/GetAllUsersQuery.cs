using MediatR;
using RentingSystemAPI.BAL.Entities;
using System.Collections.Generic;

namespace RentingSystemAPI.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }
}