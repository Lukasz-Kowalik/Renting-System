using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;

        public ItemsController(IMapper mapper, IItemService itemService, ICategoryService categoryService)
        {
            _mapper = mapper;
            _itemService = itemService;
            _categoryService = categoryService;
        }

        [HttpGet("getList")]
        public ActionResult<IEnumerable<ItemListResponse>> Get()

        {
            var list = _itemService.GatItems().ToList();
            var categories = _categoryService.Get().ToList();
            try
            {
                var response = from i in list
                               join c in categories on i.CategoryId equals c.Id
                               select new ItemListResponse { Category = c.Name, Name = i.Name, ItemId = i.ItemId, Quantity = i.Quantity };

                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _itemService.Get(id);
            var result = new ItemResponse
            {
                Url = item.DocumentationUrl,
                Category = _itemService.GetItemCategoryNameById(id),
                MaxQuantity = item.MaxQuantity,
                Quantity = item.Quantity,
                Name = item.Name
            };
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(ItemRequest item)
        {
            var newItem = _mapper.Map<Item>(item);
            _itemService.Add(newItem);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(ItemRequest item)
        {
            var newItem = _mapper.Map<Item>(item);
            _itemService.UpdateItem(newItem);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, int quantity = 0)
        {
            return _itemService.Delete(id, quantity) ? Ok() : (IActionResult)NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult Add(int id, int quantity)
        {
            _itemService.Add(id, quantity);
            return Ok();
        }
    }
}