using EFCoreMappingApp;
using Microsoft.EntityFrameworkCore;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(RezervationsDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUernameAsync(string username) =>
            await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
    }
}
