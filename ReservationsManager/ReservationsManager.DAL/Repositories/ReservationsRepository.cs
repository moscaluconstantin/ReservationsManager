using EFCoreMappingApp;
using Microsoft.EntityFrameworkCore;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain;

namespace ReservationsManager.DAL.Repositories
{
    public class ReservationsRepository : GenericRepository<Reservation>, IReservationsRepository
    {
        public ReservationsRepository(RezervationsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TimeBlock>> GetReservedTimeBlockByEmployeeIdAsync(int employeeId, DateTime date) =>
            await _context.Set<Reservation>()
                .Include(x => x.TimeBlock)
                .Include(x => x.ActionEmployee)
                .Where(x => x.ActionEmployee.EmployeeID == employeeId && x.Date.Date.CompareTo(date.Date) == 0)
                .Select(x => x.TimeBlock)
                .ToListAsync();
    }
}
