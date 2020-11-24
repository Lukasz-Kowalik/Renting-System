using AutoMapper;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystemAPI.Services
{
    public class RentService : IRentService
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly RentingContext _context;

        public RentService(ICartService cartService, IMapper mapper, IUserService userService, RentingContext context)
        {
            _cartService = cartService;
            _mapper = mapper;
            _userService = userService;
            _context = context;
        }

        public async Task<bool> Add(ClaimsPrincipal userPrincipal, string userEmail = null)
        {
            try
            {
                var userId = _userService.GetUserId(userPrincipal, userEmail);

                var cartItems = _cartService.GetUserCart(userId);
                var rentedItems = _mapper.Map<RentedItem[]>(cartItems);

                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var rents = new Rent { UserId = userId, RentedItems = rentedItems };
                    await _context.Rents.AddAsync(rents);
                    await _context.SaveChangesAsync();

                    _cartService.RemoveAllItems(userId);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public Rent GetRent(int id)
        {
            return _context.Rents.FirstOrDefault(x => x.RentId == id);
        }
    }
}