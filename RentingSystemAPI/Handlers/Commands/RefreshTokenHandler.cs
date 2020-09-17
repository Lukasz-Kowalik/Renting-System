//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using MediatR;
//using Microsoft.AspNetCore.Identity;
//using RentingSystemAPI.BAL.Entities;
//using RentingSystemAPI.Commands;
//using RentingSystemAPI.Managers;
//using RentingSystemAPI.Models;

//namespace RentingSystemAPI.Handlers.Commands
//{
//    public class RefreshTokenHandler:IRequestHandler<RefreshTokenCommand, Tokens>
//    {
//        private readonly SignInManager<User> _signInManager;
//        private readonly UserManager<User> _userManager;

//        public RefreshTokenHandler(SignInManager<User> signInManager, UserManager<User> userManager)
//        {
//            _signInManager = signInManager;
//            _userManager = userManager;
//        }

//        public async Task<Tokens> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
//        {
//            var user =await _userManager.FindByEmailAsync(request.userClaim.Value);

//            string token = user.RefreshToken;
//            if (token==request.refreshClaim.Value)
//            {
//                var refreshToken = TokenManager.GenerateRefreshToken(user);

//                user.RefreshToken=refreshToken.key;


//                await _userManager.UpdateAsync(user);
//                  //  (u => u.Id == user.Id, user);

//                return new Tokens
//                {
//                    AccessToken = TokenManager.GenerateAccessToken(user),
//                    RefreshToken = refreshToken.key
//                };
//            }
//            else
//            {
//                throw new System.Exception("Refresh token incorrect");
//            }
//        }
//    }
//}
