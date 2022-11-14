using ReservationsManager.Common.Dtos.Auth;
using ReservationsManager.Common.Dtos.Users;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IUsersService
    {
        Task AddUserAsync(UserForRegisterDto userForRegisterDto);
        public Task<IEnumerable<UserDto>> GetAllAsync();
        Task<IEnumerable<User>> GetAllNativeAsync();
    }
}
