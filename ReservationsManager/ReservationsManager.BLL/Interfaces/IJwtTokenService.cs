using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IJwtTokenService
    {
        string Generate(int id, string role);
        string Generate(string issuer, List<Claim> claims);
        int GetIssuer(string jwt);
        JwtSecurityToken Validate(string jwt);
    }
}
