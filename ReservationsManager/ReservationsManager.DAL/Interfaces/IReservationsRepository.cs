using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Interfaces
{
    public interface IReservationsRepository : IGenericRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetAllByUserIdAsync(int userId);
        Task<IEnumerable<Reservation>> GetAllOrderedByDateAsync();
        Task<IEnumerable<TimeBlock>> GetReservedTimeBlockByEmployeeIdAsync(int employeeId, DateTime date);
    }
}
