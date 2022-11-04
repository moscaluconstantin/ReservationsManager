using ReservationsManager.Domain;

namespace ReservationsManager.DAL.Interfaces
{
    public interface IReservationsRepository : IGenericRepository<Reservation>
    {
        public Task<IEnumerable<Reservation>> GetAllOrderedByDateAsync();
        public Task<IEnumerable<TimeBlock>> GetReservedTimeBlockByEmployeeIdAsync(int employeeId, DateTime date);
    }
}
