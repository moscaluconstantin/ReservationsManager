using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Exceptions;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReservationsManager.BLL.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly byte[] _key;
        private readonly int _duration;

        public JwtTokenService(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtTokenSettings>();

            _key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
            _duration = jwtSettings.Duration;
        }

        public string Generate(int id, string role)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Role, role) };
            return Generate(id.ToString(), claims);
        }

        public string Generate(string issuer, List<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(_key);
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(issuer, null, claims, null, DateTime.Now.AddHours(_duration));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public int GetIssuer(string jwt)
        {
            JwtSecurityToken token = Validate(jwt);

            return Convert.ToInt32(token.Issuer);
        }

        public JwtSecurityToken Validate(string jwt)
        {
            try
            {
                var symmetricSecurityKey = new SymmetricSecurityKey(_key);

                new JwtSecurityTokenHandler().ValidateToken(jwt, new TokenValidationParameters
                {
                    IssuerSigningKey = symmetricSecurityKey,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch (Exception)
            {
                throw new InvalidTokenException();
            }
        }
    }
}
