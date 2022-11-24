using ReservationsManager.Domain.Models;

namespace ReservationsManager.Common.Dtos.Reservations
{
    public class ReservationForUpdateDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }

        public int ActionEmployeeID { get; set; }

        public DateTime Date { get; set; }

        public int TimeBlockID { get; set; }

        public bool Canceled { get; set; }
    }
}
