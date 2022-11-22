namespace ReservationsManager.Common.Dtos.Reservations
{
    public class AvailableTimeBlocksRequestDto
    {
        public int ActionId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
    }
}
