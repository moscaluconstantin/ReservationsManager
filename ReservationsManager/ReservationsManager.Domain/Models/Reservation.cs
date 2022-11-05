namespace ReservationsManager.Domain.Models
{
    public class Reservation : Entity
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int ActionEmployeeID { get; set; }
        public ActionEmployee ActionEmployee { get; set; }

        public DateTime Date { get; set; }

        public int TimeBlockID { get; set; }
        public TimeBlock TimeBlock { get; set; }
    }
}
