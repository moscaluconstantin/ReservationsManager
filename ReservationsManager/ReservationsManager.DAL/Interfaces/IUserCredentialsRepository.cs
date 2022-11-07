using ReservationsManager.Domain.Auth;

namespace ReservationsManager.DAL.Interfaces
{
    public interface IUserCredentialsRepository : IGenericRepository<UserCredential>
    {
        Task<UserCredential> GetByLoginAsync(string login);
    }
}
