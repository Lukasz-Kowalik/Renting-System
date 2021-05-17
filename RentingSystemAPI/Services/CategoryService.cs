using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RentingSystemAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly RentingContext _context;

        public CategoryService(RentingContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public Category Get(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Category> Get()
        {
            return _context.Categories;
        }

        public string GetCategoryName(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id).Name;
        }

        public void Update(int id, string name)
        {
            var category = Get(id);
            category.Name = name;
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = Get(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}