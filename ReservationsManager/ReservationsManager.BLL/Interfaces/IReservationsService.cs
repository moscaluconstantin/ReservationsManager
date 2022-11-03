using ReservationsManager.Common.Dtos.Reservations;
using ReservationsManager.Common.Dtos.TimeBlocks;
using ReservationsManager.Domain;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IReservationsService
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<IEnumerable<TimeBlockDto>> GetAvailableTimeBlocksAsync(AvailableTimeBlocksRequestDto requestDto);
        Task AddReservation(ReservationToAddDto reservationToAddDto);
    }
}
