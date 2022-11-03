using EFCoreMappingApp;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain;

namespace ReservationsManager.DAL.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(RezervationsDbContext context) : base(context)
        {
        }
    }
}
