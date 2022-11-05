namespace ReservationsManager.Domain.Models
{
    public class ActionEmployee : Entity
    {
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public int ActionID { get; set; }
        public Action Action { get; set; }
        public int Duration { get; set; }
    }
}
