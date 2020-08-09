﻿using MediatR;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Commands;
using RentingSystemAPI.DAL.Context;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RentingSystemAPI.Queries;

namespace RentingSystemAPI.Handlers.Queries
{
    public class GetRentedItemsByUserHandler : IRequestHandler<GetRentedItemsByUserQuery, List<RentedItem>>
    {
        private readonly RentingContext _context;

        public GetRentedItemsByUserHandler(RentingContext context)
        {
            _context = context;
        }

        public async Task<List<RentedItem>> Handle(GetRentedItemsByUserQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}