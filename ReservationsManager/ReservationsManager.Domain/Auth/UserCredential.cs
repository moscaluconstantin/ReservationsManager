using ReservationsManager.Domain.Models;

namespace ReservationsManager.Domain.Auth
{
    public class UserCredential : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
