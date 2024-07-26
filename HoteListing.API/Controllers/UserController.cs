using HoteListing.API.Contracts;
using HoteListing.API.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace HoteListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<UserController> _logger;
        public UserController(IAuthManager authManager, ILogger<UserController> logger) 
        {
            _authManager = authManager;
            _logger = logger;
        }

        // POST: api/User/Register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register( [FromBody] ApiUserDto userDto)
        {
            _logger.LogInformation($"User Registration Attempt for {userDto.Email}");
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(Register)} - User Registration attemp for Email {userDto.Email}");

                return Problem($"Something went wrong in the {nameof(Register)}. Please contact Support." , statusCode: StatusCodes.Status500InternalServerError);
            }
            
        }

        // POST: api/User/Login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            _logger.LogInformation($"Login Attempt for {loginDto.Email}");
            try
            {
                var authResponse = await _authManager.Login(loginDto);
                if (authResponse == null)
                    return Unauthorized();
                return Ok(authResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(Login)} - User login attemp for Email {loginDto.Email}");
                return Problem($"Something went wrong in {nameof(Login)} - User login attemp for Email {loginDto.Email}. Please contact Support.", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/User/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);
            if (authResponse == null)
                return Unauthorized();
            return Ok(authResponse);
        }
    }
}
