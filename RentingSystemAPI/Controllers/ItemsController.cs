using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Helpers.Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly RentingContext _context;

        public ItemsController(RentingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
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