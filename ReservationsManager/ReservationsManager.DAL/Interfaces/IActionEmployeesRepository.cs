using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Interfaces
{
    public interface IActionEmployeesRepository: IGenericRepository<ActionEmployee>
    {
        public Task<IEnumerable<ActionEmployee>> GetAllByEmployeeIdAsync(int employeeId);
    }
}
