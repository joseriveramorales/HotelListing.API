using HoteListing.API.Contracts;
using HoteListing.API.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace HoteListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public UserController(IAuthManager authManager) 
        {
            _authManager = authManager;
        }

        // POST: api/User/Register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register( [FromBody] ApiUserDto userDto)
        {
            var errors = await _authManager.Register(userDto);
            if (errors.Any()) 
            {
                foreach (var error in errors)
                {
                    // Model State is what handles errors or state of the model,
                    // the model being whatever we get on the req
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        // POST: api/User/Login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var authResponse = await _authManager.Login(loginDto);
            if (authResponse == null)
                return Unauthorized();
            return Ok(authResponse);
        }


    }
}
