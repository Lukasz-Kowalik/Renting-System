using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RentingContext _context;
        private readonly string _mainWebPage = "http://localhost:3000";
        private readonly string _registerPage = "http://localhost:3000/Account/Register";

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
        public async Task<ActionResult> CreateUser([FromForm]Register register)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(_registerPage);
            }
            else
            {
                return Redirect(_mainWebPage);
            }
        }
    }
}