using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;

namespace ReservationsManager.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ActionEmployeesController : ControllerBase
    {
        private readonly IActionEmployeesService _actionEmployeesService;

        public ActionEmployeesController(IActionEmployeesService actionEmployeesService) => 
            _actionEmployeesService = actionEmployeesService;

        [HttpGet("EmployeeActions")]
        public async Task<IActionResult> GetAllByEmployeeId(int employeeId)
        {
            var actions = await _actionEmployeesService.GetAllByEmployeeIdAsync(employeeId);
            return Ok(actions);
        }
    }
}
