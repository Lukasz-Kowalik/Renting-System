//using MediatR;
//using System.Security.Claims;
//using RentingSystemAPI.Models;

//namespace RentingSystemAPI.Commands
//{
//    public class RefreshTokenCommand : IRequest<Tokens>
//    {
//        public Claim userClaim;

//        public Claim refreshClaim;

//        public RefreshTokenCommand(Claim userClaim, Claim refreshClaim)
//        {
//            this.userClaim = userClaim;
//            this.refreshClaim = refreshClaim;
//        }
//    }
//}