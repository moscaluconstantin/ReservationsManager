using EFCoreMappingApp;
using Microsoft.EntityFrameworkCore;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Auth;

namespace ReservationsManager.DAL.Repositories
{
    public class UserCredentialsRepository : GenericRepository<UserCredential>, IUserCredentialsRepository
    {
        public UserCredentialsRepository(RezervationsDbContext context) : base(context)
        {
        }

        public async Task<UserCredential> GetByLoginAsync(string login) =>
            await _context.UserCredentials.FirstOrDefaultAsync(x => x.Login == login);
    }
}
