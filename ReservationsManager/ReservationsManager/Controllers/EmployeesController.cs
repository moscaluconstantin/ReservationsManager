using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;

namespace ReservationsManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeesService.GetAllAsync();
            return Ok(employees);
        }
    }
}
