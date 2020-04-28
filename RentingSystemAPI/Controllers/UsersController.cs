using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Commands;
using RentingSystemAPI.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
            var result = await _mediator.Send(query);
         return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);
            Debug.WriteLine(result.Password);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
        /// <summary>
        /// Creates user account.
        /// </summary>
        /// <remarks>
        /// Sample user data:
        ///
        ///    
        ///     {
        ///          "FirstName":"dsf",
        ///          "LastName":"dsfa",
        ///          "Email":"adsl@gmai.com",
        ///          "PasswordHash":"+NSQGZzpjLxSzpyK03ToNGOlbwQ=",
        ///          "Salt":"4IQ6cUgEz6E/997WLIB8ng=="
        ///     }
        ///
        /// </remarks>
        /// <param name="registeredUser"></param>
        /// <returns>Response code</returns>
        [HttpPost]
        [Produces("application/json")]
        [Route("/CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateUser(Object registeredUser)
        {
            var command = new CreateUserCommand(registeredUser);
            var result = await _mediator.Send(registeredUser);
            //if (registeredUser==String.Empty)
            //{
            //    return NotFound();
            //}
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