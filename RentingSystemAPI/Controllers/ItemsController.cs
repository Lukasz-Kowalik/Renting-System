using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Interfaces;
using RentingSystemAPI.Validators;
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

        [HttpPost("GetSortedList")]
        public ActionResult<IEnumerable<ItemListResponse>> Get([FromForm] int[] ids)

        {
            var list = _itemService.GatItems().ToList();
            var categories = _categoryService.Get(ids).ToList();
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
            try
            {
                var item = _itemService.Get(id);
                var result = new ItemResponse
                {
                    Id = item.ItemId,
                    Url = item.DocumentationUrl,
                    ImageUrl = item.ImageUrl,
                    Description = item.Description,
                    Category = _mapper.Map<CategoryResponse>(_categoryService.Get(item.CategoryId)),
                    MaxQuantity = item.MaxQuantity,
                    Quantity = item.Quantity,
                    Name = item.Name
                };
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Add([FromForm] ItemRequest item)
        {
            var validator = new ItemValidator();
            var validationResult = validator.Validate(item);
            if (validationResult.IsValid)
            {
                var newItem = _mapper.Map<Item>(item);
                _itemService.Add(newItem);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromForm] ItemRequest item)
        {
            var validator = new ItemValidator();
            var validationResult = validator.Validate(item);
            if (validationResult.IsValid)
            {
                var oldItem = _itemService.Get(item.ItemId);
                oldItem.ItemId = item.ItemId;
                oldItem.Name = item.Name;
                oldItem.DocumentationUrl = item.Url;
                oldItem.ImageUrl = item.ImageUrl;
                oldItem.MaxQuantity = item.MaxQuantity;
                oldItem.Description = item.Description;
                oldItem.CategoryId = item.CategoryId;

                _itemService.UpdateItem(oldItem);
                return Ok();
            }
            return BadRequest();
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