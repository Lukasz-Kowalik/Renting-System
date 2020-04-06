using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using MediatR;

namespace RentingSystemAPI.Queries
{
    public class GetUserByIdQuery:IRequest<User>
    {
        public int UserId { get;  }

        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
