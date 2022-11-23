namespace ReservationsManager.Common.Dtos.Reservations
{
    public class ReservationToAddDto
    {
        public int UserId { get; set; }
        public int ActionEmployeeId { get; set; }
        public int TimeBlockId { get; set; }
        public DateTime Date { get; set; }
    }
}
