using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Commands;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace RentingSystemAPI.Handlers.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly RentingContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CreateUserHandler(RentingContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = JsonConvert.DeserializeObject<UserDto>(request.Json.ToString() ?? string.Empty);
            var user = _mapper.Map<User>(userDto);
            user.UserType = (int)AccountTypes.Customer;
            var result = await _userManager.CreateAsync(user);
            return result;
        }
    }
}