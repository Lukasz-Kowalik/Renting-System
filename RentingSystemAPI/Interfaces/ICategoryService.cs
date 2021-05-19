using RentingSystemAPI.BAL.Entities;
using System.Collections.Generic;

namespace RentingSystemAPI.Interfaces
{
    public interface ICategoryService
    {
        string GetCategoryName(int id);

        Category Get(int id);

        IEnumerable<Category> Get();

        IEnumerable<Category> Get(int[] ids = null);

        void Add(Category category);

        bool Update(int id, string name);

        void Delete(int id);
    }
}