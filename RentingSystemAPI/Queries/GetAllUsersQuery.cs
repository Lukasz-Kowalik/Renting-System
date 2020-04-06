using System.Collections.Generic;
using DAL.Models;
using MediatR;

namespace RentingSystemAPI.Controllers
{
    public class GetAllUsersQuery:IRequest<List<User>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}