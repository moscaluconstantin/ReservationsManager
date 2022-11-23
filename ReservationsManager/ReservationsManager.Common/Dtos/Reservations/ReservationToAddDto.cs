namespace ReservationsManager.Common.Dtos.Reservations
{
    public class ReservationToAddDto
    {
        public int UserId { get; set; }
        public int ActionId { get; set; }
        public int EmployeeId { get; set; }
        public int StartTimeBlockId { get; set; }
        public DateTime Date { get; set; }
    }
}
