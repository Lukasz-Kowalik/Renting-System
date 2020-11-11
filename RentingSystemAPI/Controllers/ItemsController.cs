using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly RentingContext _context;
        private readonly IMapper _mapper;

        public ItemsController(RentingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
    }
}