using EFCoreMappingApp;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Repositories
{
    public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(RezervationsDbContext context) : base(context)
        {
        }
    }
}
