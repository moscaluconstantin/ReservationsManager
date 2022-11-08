using ReservationsManager.Common;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IAuthenticateService
    {
        Task<string> Login(LoginModel userForLoginDto);
        Task Register(RegisterModel userForRegisterDtio, string role);
    }
}
