namespace ReservationsManager.Domain.Models
{
    public class Employee : Entity
    {
        public string UserName { get; set; }
        public ICollection<Action> Actions { get; set; }
    }
}
