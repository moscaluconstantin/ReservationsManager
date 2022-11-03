using ReservationsManager.Common.Dtos.Users;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IUsersService
    {
        public Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
