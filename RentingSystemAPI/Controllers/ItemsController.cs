using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly RentingContext _context;
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;

        public ItemsController(RentingContext context, IMapper mapper, IItemService itemService)
        {
            _context = context;
            _mapper = mapper;
            _itemService = itemService;
        }

        [HttpGet("getList")]
        public async Task<ActionResult<IEnumerable<ItemListResponse>>> GetItemToList()

        {
            var list = _context.Items.ToList();
            var categories = _context.Categories.ToList();

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
        public IActionResult GetItem(int id)
        {
            var item = _context.Items.FirstOrDefault(x => x.ItemId == id);
            var result = new ItemResponse
            {
                Url = item.DocumentationURL,
                Category = _itemService.GetItemCategoryNameById(id),
                MaxQuantity = item.MaxQuantity,
                Quantity = item.Quantity,
                Name = item.Name
            };
            return Ok(result);
        }
    }
}