using DataLogic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Renting_System.Models;
using RentingSystemAPI.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RentingContext _context;

        public UsersController(RentingContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        [Route("/CreateUser")]
        public async Task<Register> CreateUser(Register register)
        {
            return register;
            //Debug.WriteLine("created");
        }
    }
}