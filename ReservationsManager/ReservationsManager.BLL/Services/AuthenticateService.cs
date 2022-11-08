using Microsoft.AspNetCore.Identity;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common;
using ReservationsManager.Common.Exceptions;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReservationsManager.BLL.Services
{
    public class AuthenticateService: IAuthenticateService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticateService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> Login(LoginModel userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, userForLoginDto.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                return _jwtTokenService.Generate(user.Id, authClaims);
            }

            return string.Empty;
        }

        public async Task Register(RegisterModel userForRegisterDto, string role)
        {
            var userExists = await _userManager.FindByNameAsync(userForRegisterDto.Username);

            if (userExists != null)
                throw new RegisterExistingUserException();

            IdentityUser user = new()
            {
                Email = userForRegisterDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userForRegisterDto.Username
            };

            var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (!result.Succeeded)
                throw new InvalidCredentialsException();

            await AddRoleToUser(user, role);
        }

        private async Task AddRoleToUser(IdentityUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            if (await _roleManager.RoleExistsAsync(role))
                await _userManager.AddToRoleAsync(user, role);
        }
    }
}
