using ReservationsManager.Common.Dtos.ActionEmployees;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IActionEmployeesService
    {
        Task<IEnumerable<ActionEmployeeDto>> GetAllByEmployeeIDAsync(int employeeID);
    }
}
