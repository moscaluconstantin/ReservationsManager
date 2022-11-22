using EFCoreMappingApp;
using Microsoft.EntityFrameworkCore;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;
using Action = ReservationsManager.Domain.Models.Action;

namespace ReservationsManager.DAL.Repositories
{
    public class ActionEmployeesRepository : GenericRepository<ActionEmployee>, IActionEmployeesRepository
    {
        public ActionEmployeesRepository(RezervationsDbContext context) : base(context)
        {
        }

        public async Task<ActionEmployee> GetByIdsAsync(int actionId, int employeeId) =>
            await _context.ActionEmployees.FirstOrDefaultAsync(x => x.EmployeeID == employeeId && x.ActionID == actionId);

        public async Task<IEnumerable<Action>> GetActionsAsync() =>
            await _context.ActionEmployees
            .Include(x => x.Action)
            .Select(x => x.Action)
            .Distinct()
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<IEnumerable<ActionEmployee>> GetAllByEmployeeIdAsync(int employeeId) =>
            await _context.ActionEmployees
            .Include(x => x.Action)
            .Where(x => x.EmployeeID == employeeId)
            .ToListAsync();

        public async Task<IEnumerable<Employee>> GetEmployeesByActionIdAsync(int actionId) =>
            await _context.ActionEmployees
            .Include(x => x.Employee)
            .Where(x=>x.ActionID==actionId)
            .Select(x => x.Employee)
            .Distinct()
            .ToListAsync();
    }
}
