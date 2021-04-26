using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.Interfaces
{
    public interface IItemService
    {
        string GetItemNameById(int id);

        Item GetItem(int id);

        string GetItemCategoryNameById(int id);
    }
}