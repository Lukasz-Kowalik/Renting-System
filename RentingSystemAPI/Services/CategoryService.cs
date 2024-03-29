﻿using RentingSystemAPI.BAL.Entities;
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

        public IEnumerable<Category> Get(int[] ids = null)
        {
            return ids.Length == 0 ? _context.Categories :
             _context.Categories.Where(x => ids.Any(y => y == x.Id));
        }

        public string GetCategoryName(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id).Name;
        }

        public bool Update(int id, string name)
        {
            var category = Get(id);
            if (category is null)
            {
                return false;
            }
            category.Name = name;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            var category = Get(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}