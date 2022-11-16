using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> GetByUernameAsync(string username);
    }
}
