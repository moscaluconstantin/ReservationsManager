namespace ReservationsManager.Domain
{
    public class Action : Entity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
