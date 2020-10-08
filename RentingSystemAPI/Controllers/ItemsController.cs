using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Helpers.Attributes;

namespace RentingSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly RentingContext _context;
        private readonly IMapper _mapper;

        public ItemsController(RentingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        [HttpGet("getList")]
        public async Task<ActionResult<IEnumerable<ItemListResponse>>> GetItemToList()
        {
            var list = await _context.Items.ToListAsync();
            try
            {
                var response = _mapper.Map<List<Item>, List<ItemListResponse>>(list);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetRents(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.ItemId == id);
            if (item == null)
            {
                return NotFound(item);
            }
            return Ok(item);
        }
    }
}