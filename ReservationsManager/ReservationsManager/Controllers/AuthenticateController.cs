using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common;
using ReservationsManager.Common.Dtos.Auth;
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
            var loginResponse = await _authService.LoginAsync(model);

            if (loginResponse==null)
                return Unauthorized();

            return Ok(loginResponse);
        }

        [HttpPost]
        [Route("Register/Employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeForRegisterDto employeeForRegister)
        {
            try
            {
                await _authService.RegisterEmployeeAsync(employeeForRegister);
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

        [HttpPost]
        [Route("Register/User")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegisterDto userForRegister)
        {
            try
            {
                await _authService.RegisterUserAsync(userForRegister);
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
            await _authService.CheckUsernameAvailabilityAsync(username);
    }
}
