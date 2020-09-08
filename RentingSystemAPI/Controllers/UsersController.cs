using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Commands;
using RentingSystemAPI.Models.Requests;
using RentingSystemAPI.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentingSystemAPI.DTOs.Request;

namespace RentingSystemAPI.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);
            if (!result.Any())
            {
                NoContent();
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NoContent();
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
        ///          "Password": "QWE12#asd"
        ///     }
        ///
        ///
        /// </remarks>
        /// <param name="registeredUser"></param>
        /// <returns>Response code</returns>

        [HttpPost]
        [Produces("application/json")]
        [Route("/RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest registeredUser)
        {
            var command = new RegisterUserCommand(registeredUser);
            var result = await _mediator.Send(command);
            if (result.Succeeded)
            {
                return Ok();
            }

            var errorMessage = result.Errors.FirstOrDefault().Code;
            return errorMessage switch
            {
                string message when message.Contains("Duplicate") => Conflict(),
                _ => NotFound()
            };
        }

        //to do
        //creater jwt

        [HttpPost]
        [Produces("application/json")]
        [Route("/Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest loginRequest)
        {
            try
            {
                var command = new LoginCommand(loginRequest);
                //user
                var result = await _mediator.Send(command);
                return Ok(result);
                //return Redirect(redirect_uri);
            }
            catch (ArgumentNullException e)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        //[Authorize(AuthenticationSchemes = "refresh")]
        //[HttpPut("accesstoken", Name = "refresh")]
        //public IActionResult Refresh()
        //{
        //    // Get the value of the claims in the token like this:
        //    Claim refreshtoken = User.Claims.FirstOrDefault(x => x.Type == "refresh");
        //    Claim username = User.Claims.FirstOrDefault(x => x.Type == "username");
        //    try
        //    {
        //        var token = TokenManager.RefreshAsync(_mediator, username, refreshtoken).Result;
        //        return Ok(token);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}