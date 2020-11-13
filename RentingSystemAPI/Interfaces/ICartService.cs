using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs.Request;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystemAPI.Interfaces
{
    public interface ICartService
    {
        IEnumerable<Item> GetUserCart(int userId);

        Task AddItemToCart(AddItemRequest request, ClaimsPrincipal user);

        Task RemoveFromCart(RemoveItemFromCartRequest request, ClaimsPrincipal user);

        Cart GetUserCartItem(int userId, int itemId);

        void RemoveAllItems(int userId);
    }
}