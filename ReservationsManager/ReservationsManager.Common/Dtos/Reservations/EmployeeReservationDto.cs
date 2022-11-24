namespace ReservationsManager.Common.Dtos.Reservations
{
    public class EmployeeReservationDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ActionName { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public bool Canceled { get; set; }
    }
}
