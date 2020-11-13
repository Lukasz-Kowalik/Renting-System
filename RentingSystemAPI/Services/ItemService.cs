using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.Interfaces;
using System.Linq;

namespace RentingSystemAPI.Services
{
    public class ItemService : IItemService
    {
        private readonly RentingContext _context;

        public ItemService(RentingContext context)
        {
            _context = context;
        }

        public string GetItemNameById(int id)
        {
            return _context.Items.FirstOrDefault(i => i.ItemId == id)?.Name;
        }
    }
}