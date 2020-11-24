using MediatR;
using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int UserId { get; }

        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}