using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common;

namespace ReservationsManager.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService) =>
            _usersService = usersService;

        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersService.GetAllNativeAsync();
            return Ok(users);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("UserForGreet/{id}")]
        public async Task<IActionResult> GetUserForGreet(int id)
        {
            var userForGreet = await _usersService.GetUserForGreet(id);

            if (userForGreet == null)
                return NotFound("Can't found user.");

            return Ok(userForGreet);
        }
    }
}
