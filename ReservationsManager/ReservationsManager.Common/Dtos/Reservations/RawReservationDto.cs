using ReservationsManager.Domain.Models;
using Action = ReservationsManager.Domain.Models.Action;

namespace ReservationsManager.Common.Dtos.Reservations
{
    public class RawReservationDto
    {
        public User User { get; set; }
        public Employee Employee { get; set; }
        public Action Action { get; set; }
        public DateTime Date { get; set; }
        public TimeBlock StartTimeBlock { get; set; }
        public TimeBlock EndTimeBlock { get; set; }
    }
}
