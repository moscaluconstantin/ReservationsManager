using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;

namespace ReservationsManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService) => 
            _usersService = usersService;

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersService.GetAllAsync();
            return Ok(users);
        }
    }
}
