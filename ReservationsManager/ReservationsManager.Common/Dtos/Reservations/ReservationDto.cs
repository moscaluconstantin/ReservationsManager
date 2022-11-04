namespace ReservationsManager.Common.Dtos.Reservations
{
    public class ReservationDto
    {
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
