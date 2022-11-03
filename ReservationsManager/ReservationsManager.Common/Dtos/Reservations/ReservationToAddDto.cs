namespace ReservationsManager.Common.Dtos.Reservations
{
    public class ReservationToAddDto
    {
        public int UserId { get; set; }
        public int ActionEmployeeId { get; set; }
        public int StartTimeBlockId { get; set; }
        public DateTime Date { get; set; }
    }
}
