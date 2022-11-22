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

        public async Task<IEnumerable<Action>> GetActionsAsync() =>
            await _context.ActionEmployees
            .Include(x => x.Action)
            .Select(x => x.Action)
            .Distinct()
            .ToListAsync();

        public async Task<IEnumerable<ActionEmployee>> GetAllByEmployeeIdAsync(int employeeId) =>
            await _context.ActionEmployees
            .Include(x => x.Action)
            .Where(x => x.EmployeeID == employeeId)
            .ToListAsync();

        public async Task<IEnumerable<Employee>> GetEmployeesAsync() =>
            await _context.ActionEmployees
            .Include(x => x.Employee)
            .Select(x => x.Employee)
            .Distinct()
            .ToListAsync();
    }
}
