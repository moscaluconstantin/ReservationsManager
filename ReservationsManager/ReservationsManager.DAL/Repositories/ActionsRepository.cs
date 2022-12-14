using EFCoreMappingApp;
using ReservationsManager.DAL.Interfaces;
using Action = ReservationsManager.Domain.Models.Action;

namespace ReservationsManager.DAL.Repositories
{
    public class ActionsRepository : GenericRepository<Action>, IActionsRepository
    {
        public ActionsRepository(RezervationsDbContext context) : base(context)
        {
        }
    }
}
