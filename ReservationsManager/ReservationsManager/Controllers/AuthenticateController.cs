using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common;
using ReservationsManager.Common.Exceptions;

namespace ReservationsManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authService;

        public AuthenticateController(IAuthenticateService authService) =>
            _authService = authService;

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var jwtToken = await _authService.Login(model);

            if (string.IsNullOrEmpty(jwtToken))
            {
                return Unauthorized();
            }

            return Ok(new { accessToken = jwtToken });
        }

        [HttpPost]
        [Route("Register/Employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterModel model)
        {
            try
            {
                await _authService.Register(model, UserRoles.Employee);
            }
            catch (RegisterExistingUserException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }
            catch(InvalidCredentialsException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Register/User")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            try
            {
                await _authService.Register(model, UserRoles.User);
            }
            catch (RegisterExistingUserException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }
            catch (InvalidCredentialsException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpGet]
        [Route("IsAvailable/{username}")]
        public async Task<bool> IsAvailable(string username) =>
            await _authService.CheckUsernameAvailability(username);
    }
}
