using RentingSystemAPI.BAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentingSystemAPI.Interfaces
{
    public interface IItemService
    {
        string GetItemNameById(int id);

        Item Get(int id);

        string GetItemCategoryNameById(int id);

        IEnumerable<Item> GatItems();

        Task Add(Item item);

        void Remove(int id);

        void Remove(Item item);

        void UpdateItem(Item item);

        bool Delete(int id, int quantity);

        void Add(int id, int quantity);
    }
}