using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;

namespace ReservationsManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionEmployeesController : ControllerBase
    {
        private readonly IActionEmployeesService _actionEmployeesService;

        public ActionEmployeesController(IActionEmployeesService actionEmployeesService) => 
            _actionEmployeesService = actionEmployeesService;

        [HttpGet]
        public async Task<IActionResult> GetAllByEmployeeId(int employeeId)
        {
            var actions = await _actionEmployeesService.GetAllByEmployeeIDAsync(employeeId);
            return Ok(actions);
        }
    }
}
