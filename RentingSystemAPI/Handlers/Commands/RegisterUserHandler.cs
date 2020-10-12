using MediatR;
using Microsoft.AspNetCore.Identity;
using RentingSystemAPI.Commands;
using RentingSystemAPI.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RentingSystemAPI.Handlers.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly IUserService _userService;

        public RegisterUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RegisterAsync(request.User);
        }
    }
}