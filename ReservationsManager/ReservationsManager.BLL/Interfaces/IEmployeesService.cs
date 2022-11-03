using ReservationsManager.Common.Dtos.Employees;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IEmployeesService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
    }
}
