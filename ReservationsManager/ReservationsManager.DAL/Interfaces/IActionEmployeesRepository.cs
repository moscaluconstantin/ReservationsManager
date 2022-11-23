using ReservationsManager.Domain.Models;
using Action = ReservationsManager.Domain.Models.Action;

namespace ReservationsManager.DAL.Interfaces
{
    public interface IActionEmployeesRepository: IGenericRepository<ActionEmployee>
    {
        Task<IEnumerable<Action>> GetActionsAsync();
        Task<IEnumerable<ActionEmployee>> GetAllByEmployeeIdAsync(int employeeId);
        Task<ActionEmployee> GetByIdsAsync(int actionId, int employeeId);
        Task<IEnumerable<ActionEmployee>> GetAllByActionIdAsync(int actionId);
    }
}
