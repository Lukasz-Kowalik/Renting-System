using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly RentingContext _context;

        public RentsController(RentingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rent>>> GetRents()
        {
            return await _context.Rents.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Rent>>> GetRents(int id)
        {
            var rent = await _context.Rents.Where(x => x.UserId == id).ToListAsync();
            if (rent.Count == 0)
            {
                return NotFound(rent);
            }
            return Ok(rent);
        }
    }
}