using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;

namespace ReservationsManager.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IActionEmployeesService _actionEmployeesService;

        public EmployeesController(IEmployeesService employeesService, IActionEmployeesService actionEmployeesService)
        {
            _employeesService = employeesService;
            _actionEmployeesService = actionEmployeesService;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeesService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("AssignedToAction/{actionId}")]
        public async Task<IActionResult> GetWorkingEmployees(int actionId)
        {
            var employees = await _actionEmployeesService.GetWorkingEmployeesAsync(actionId);
            return Ok(employees);
        }
    }
}
