using EFCoreMappingApp;
using Microsoft.EntityFrameworkCore;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Repositories
{
    public class ReservationsRepository : GenericRepository<Reservation>, IReservationsRepository
    {
        public ReservationsRepository(RezervationsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reservation>> GetAllByEmployeeIdAsync(int employeeId) =>
            await _context.Reservations
            .Include(x => x.ActionEmployee.Action)
            .Include(x => x.User)
            .Include(x => x.TimeBlock)
            .Where(x => x.ActionEmployee.EmployeeID == employeeId)
            .OrderBy(x => x.Date)
            .ToListAsync();

        public async Task<IEnumerable<Reservation>> GetAllByUserIdAsync(int userId) =>
            await _context.Reservations
            .Include(x => x.ActionEmployee.Employee)
            .Include(x => x.ActionEmployee.Action)
            .Include(x => x.TimeBlock)
            .OrderBy(x => x.Date)
            .ToListAsync();

        public async Task<IEnumerable<Reservation>> GetAllOrderedByDateAsync() =>
            await _context.Reservations
            .Include(x => x.User)
            .Include(x => x.ActionEmployee.Employee)
            .Include(x => x.TimeBlock)
            .OrderBy(x => x.Date)
            .ToListAsync();

        public async Task<IEnumerable<TimeBlock>> GetReservedTimeBlockByEmployeeIdAsync(int employeeId, DateTime date) =>
            await _context.Reservations
                .Include(x => x.TimeBlock)
                .Include(x => x.ActionEmployee)
                .Where(x => x.ActionEmployee.EmployeeID == employeeId && x.Date.Date.CompareTo(date.Date) == 0)
                .Select(x => x.TimeBlock)
                .ToListAsync();
    }
}
