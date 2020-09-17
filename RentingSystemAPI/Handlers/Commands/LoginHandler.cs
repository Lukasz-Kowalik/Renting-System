using MediatR;
using RentingSystemAPI.Commands;
using RentingSystemAPI.Interfaces;
using RentingSystemAPI.Models.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace RentingSystemAPI.Handlers.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthenticateResponse>
    {
        private readonly IUserService _userService;

        public LoginHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AuthenticateResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.AuthenticateAsync(request.Json);
            return result;
        }
    }
}