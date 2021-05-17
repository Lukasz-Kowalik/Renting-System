using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentingSystemAPI.Services
{
    public class ItemService : IItemService
    {
        private readonly RentingContext _context;
        private readonly ICategoryService _categoryService;

        public ItemService(RentingContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        public string GetItemNameById(int id)
        {
            return _context.Items.FirstOrDefault(i => i.ItemId == id)?.Name;
        }

        public Item Get(int id)
        {
            return _context.Items.FirstOrDefault(x => x.ItemId == id);
        }

        public IEnumerable<Item> GatItems()
        {
            return _context.Items;
        }

        public string GetItemCategoryNameById(int id)
        {
            var item = Get(id);
            return _categoryService.GetCategoryName(item.CategoryId);
        }

        public async Task Add(Item item)
        {
            item.RentedItems = new List<RentedItem>();
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            var item = Get(id);
            _context.Items.Remove(item);
            _context.SaveChanges();
        }

        public void Remove(Item item)
        {
            _context.Items.Remove(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            _context.Update(item);
            _context.SaveChanges();
        }

        public bool Delete(int id, int quantity)
        {
            var item = Get(id);
            //remove items from db
            if (quantity == 0)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
                return true;
            }

            if (item.Quantity > quantity)
            {
                item.Quantity -= quantity;
                item.MaxQuantity -= quantity;
                _context.Items.Remove(item);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public void Add(int id, int quantity)
        {
            var item = Get(id);
            if (item != null)
            {
                item.Quantity += quantity;
                item.MaxQuantity += quantity;
                _context.Items.Update(item);
                _context.SaveChanges();
            }
        }
    }
}