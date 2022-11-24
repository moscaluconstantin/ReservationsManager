namespace ReservationsManager.Domain.Models
{
    public class Employee : RegistrationsUser
    {
        public DateTime ExperienceStartDate { get; set; }
        public string Description { get; set; }
        public ICollection<Action> Actions { get; set; }
    }
}
