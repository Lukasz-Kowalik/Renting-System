using MediatR;
using RentingSystemAPI.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.Handlers.Commands
{
    public class FindUserHandler : IRequestHandler<FindUserCommand, Object>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public FindUserHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<object> Handle(FindUserCommand request, CancellationToken cancellationToken)
        {
            //json =>objekt
            throw new NotImplementedException();
        }
    }
}