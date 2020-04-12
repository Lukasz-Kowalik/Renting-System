using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Queries;

namespace RentingSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            var query = new GetAllUsersQuery();
            var result =await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query); 
            return result != null ?(IActionResult) Ok(result) : NotFound();
           }

        [HttpPost]
        [Route("/CreateUser")]
        public async Task<ActionResult> CreateUser(string registeredUser)
        {
            if (registeredUser==String.Empty)
            {
                return NotFound();
            }
            //if (!ModelState.IsValid)
            //{
            //    return Redirect(_registerPage);
            //}
            //else
            //{
            //    return Redirect(_mainWebPage);
            //}
            return Ok();
        }
    }
}