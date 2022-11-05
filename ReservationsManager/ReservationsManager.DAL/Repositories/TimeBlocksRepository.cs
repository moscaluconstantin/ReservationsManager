using EFCoreMappingApp;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Repositories
{
    public class TimeBlocksRepository : GenericRepository<TimeBlock>, ITimeBlocksRepository
    {
        public TimeBlocksRepository(RezervationsDbContext context) : base(context)
        {
        }
    }
}
