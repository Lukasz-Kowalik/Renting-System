using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Commands;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.Interfaces;
using RentingSystemAPI.Models.Requests;
using RentingSystemAPI.Models.Responses;
using RentingSystemAPI.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public UsersController(IMediator mediator,
          IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);
            if (result.Count <= 0)
            {
                NoContent();
            }
            return Ok(result);
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

            var errorMessage = result.Errors.FirstOrDefault()?.Code;
            return errorMessage switch
            {
                string message when message.Contains("Duplicate") => Conflict(),
                _ => NotFound()
            };
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        ///
        ///     {
        ///          "Email":"adsl@gmai.com",
        ///          "Password": "QWE12#asd"
        ///     }
        ///
        /// </remarks>
        /// <param name="loginRequest"></param>
        /// <returns>AuthenticateResponse</returns>
        [HttpPost]
        [Route("/Login")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest loginRequest)
        {
            try
            {
                var command = new LoginCommand(loginRequest);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentNullException e)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();

            return Ok();
        }

        [HttpGet]
        [Route("/Token")]
        public async Task<IActionResult> Token(string email)
        {
            try
            {
                var user = await _userService.GetUserAsync(User, email);
                var result = await _userService.Refresh(user);
                return Ok(result);
            }
            catch (ArgumentNullException e)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPatch]
        [Route("SeedUserRoles")]
        public async Task<IActionResult> SeedRoles()
        {
            await _userService.SeedRolesAsync();
            return Ok();
        }
    }
}