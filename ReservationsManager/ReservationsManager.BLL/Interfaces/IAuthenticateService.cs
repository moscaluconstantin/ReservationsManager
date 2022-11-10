using ReservationsManager.Common;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IAuthenticateService
    {
        Task<bool> CheckUsernameAvailability(string username);
        Task<string> Login(LoginModel userForLoginDto);
        Task Register(RegisterModel userForRegisterDtio, string role);
    }
}
