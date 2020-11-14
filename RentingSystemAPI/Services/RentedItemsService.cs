using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystemAPI.Services
{
    public class RentedItemsService : IRentedItemsService
    {
        private readonly IUserService _userService;
        private readonly RentingContext _context;
        private readonly IItemService _itemService;
        private readonly IRentService _rentService;

        public RentedItemsService(IUserService userService, RentingContext context,
            IItemService itemService, IRentService rentService)
        {
            _userService = userService;
            _context = context;
            _itemService = itemService;
            _rentService = rentService;
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
                        RentReturnTime = rent.RentReturnTime,
                        IsReturned = rentedItem.IsReturned
                    });
                }
            }

            return list;
        }

        public async Task<bool> ReturnItems(ClaimsPrincipal userPrincipal, ReturnItemsRequest request)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var rentedItem = _context.RentedItems
                    .FirstOrDefault(x => x.ItemId == request.ItemId && x.RentId == request.RentId);
                rentedItem.IsReturned = true;
                _context.RentedItems.Update(rentedItem);
                await _context.SaveChangesAsync();

                var item = _itemService.GetItem(request.ItemId);
                item.Quantity += rentedItem.Quantity;
                _context.Items.Update(item);
                await _context.SaveChangesAsync();

                var allItemsAreReturned = await _context.RentedItems
                    .Where(x => x.RentId == request.RentId)
                    .AllAsync(x => x.IsReturned);
                if (allItemsAreReturned)
                {
                    var rent = _rentService.GetRent(request.RentId);
                    rent.RentReturnTime = DateTime.Now;
                    _context.Rents.Update(rent);
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }
    }
}