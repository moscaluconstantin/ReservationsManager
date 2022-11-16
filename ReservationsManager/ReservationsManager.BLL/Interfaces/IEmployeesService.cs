using ReservationsManager.Common.Dtos.Auth;
using ReservationsManager.Common.Dtos.Employees;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IEmployeesService : IGetIdService
    {
        Task AddEmployeeAsync(EmployeeForRegisterDto employeeForRegisterDto);
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
    }
}
