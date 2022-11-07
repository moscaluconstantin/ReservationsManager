using System.IdentityModel.Tokens.Jwt;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IJwtTokenService
    {
        string Generate(int id, string role);
        int GetIssuer(string jwt);
        JwtSecurityToken Validate(string jwt);
    }
}
