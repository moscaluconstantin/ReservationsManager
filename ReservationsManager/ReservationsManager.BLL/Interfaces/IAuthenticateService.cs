using ReservationsManager.Common;
using ReservationsManager.Common.Dtos.Auth;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IAuthenticateService
    {
        Task<bool> CheckUsernameAvailability(string username);
        Task<string> Login(LoginModel userForLoginDto);
        Task Register(RegisterModel userForRegisterDtio, string role);
        Task RegisterUser(UserForRegisterDto userForRegisterDto);
    }
}
