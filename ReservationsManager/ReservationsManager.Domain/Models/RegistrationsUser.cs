namespace ReservationsManager.Domain.Models
{
    public class RegistrationsUser : Entity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
