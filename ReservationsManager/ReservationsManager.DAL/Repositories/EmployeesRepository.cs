using EFCoreMappingApp;
using Microsoft.EntityFrameworkCore;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Repositories
{
    public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(RezervationsDbContext context) : base(context)
        {
        }

        public async Task<Employee> GetByUsernameAsync(string username) =>
            await _context.Employees.FirstOrDefaultAsync(x => x.UserName == username);
    }
}
