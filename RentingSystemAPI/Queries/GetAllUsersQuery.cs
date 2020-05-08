using System.Collections.Generic;
using MediatR;
using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.Queries
{
    public class GetAllUsersQuery:IRequest<List<User>>
    {
     
    }
}