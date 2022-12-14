namespace ReservationsManager.Common.Dtos.Reservations
{
    public class UserReservationDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string ActionName { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public bool Canceled { get; set; }
    }
}
