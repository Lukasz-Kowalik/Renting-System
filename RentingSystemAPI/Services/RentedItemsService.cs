using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RentingSystemAPI.Services
{
    public class RentedItemsService : IRentedItemsService
    {
        private readonly IUserService _userService;
        private readonly RentingContext _context;
        private readonly IItemService _itemService;

        public RentedItemsService(IUserService userService, RentingContext context,
            IItemService itemService)
        {
            _userService = userService;
            _context = context;
            _itemService = itemService;
        }

        public IEnumerable<RentedItemsResponse> Get(ClaimsPrincipal userPrincipal, string userEmail = null)
        {
            var userId = _userService.GetUserId(userPrincipal, userEmail);

            var rents = _context.Rents.Where(r => r.UserId == userId).Include(x => x.RentedItems).ToList();

            List<RentedItemsResponse> list = new List<RentedItemsResponse>();
            foreach (var rent in rents)
            {
                foreach (var rentedItem in rent.RentedItems)
                {
                    var itemId = rentedItem.ItemId;

                    list.Add(new RentedItemsResponse
                    {
                        RentId = rent.RentId,
                        ItemId = itemId,
                        Name = _itemService.GetItemNameById(itemId),
                        Quantity = rentedItem.Quantity,
                        RentTime = rent.RentTime,
                        WhenShouldBeReturned = rent.WhenShouldBeReturned,
                        RentReturnTime = rent.RentReturnTime
                    });
                }
            }

            return list;
        }
    }
}