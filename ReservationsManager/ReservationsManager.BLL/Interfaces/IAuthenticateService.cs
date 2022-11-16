using ReservationsManager.Common;
using ReservationsManager.Common.Dtos.Auth;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IAuthenticateService
    {
        Task<bool> CheckUsernameAvailabilityAsync(string username);
        Task<LoginResponseDto> LoginAsync(LoginModel userForLoginDto);
        Task RegisterEmployeeAsync(EmployeeForRegisterDto employeeForRegisterDto);
        Task RegisterUserAsync(UserForRegisterDto userForRegisterDto);
    }
}
