using ReservationsManager.Common.Dtos.Reservations;
using ReservationsManager.Common.Dtos.TimeBlocks;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IReservationsService
    {
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<IEnumerable<TimeBlockDto>> GetAvailableTimeBlocksAsync(AvailableTimeBlocksRequestDto requestDto);
        Task AddReservation(ReservationToAddDto reservationToAddDto);
        Task<IEnumerable<UserReservationDto>> GetAllByUserIdAsync(int userId);
        Task<IEnumerable<EmployeeReservationDto>> GetAllByEmployeeIdAsync(int employeeId);
        Task UpdateReservation(ReservationCanceledUpdateDto updateDto);
    }
}
