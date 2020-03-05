using DataLogic.Model;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentingSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly RentingContext _context;

        public ItemsController(RentingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }
    }
}