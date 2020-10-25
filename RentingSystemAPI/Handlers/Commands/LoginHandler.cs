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
            return await _userService.LoginAsync(request.Json);
        }
    }
}