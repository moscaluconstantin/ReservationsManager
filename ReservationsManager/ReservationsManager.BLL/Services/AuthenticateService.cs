using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common;
using ReservationsManager.Common.Dtos.Auth;
using ReservationsManager.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReservationsManager.BLL.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUsersService _usersService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AuthenticateService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUsersService usersService,
            IJwtTokenService jwtTokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _usersService = usersService;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
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

        public async Task RegisterUser(UserForRegisterDto userForRegisterDto)
        {
            var registerDto = _mapper.Map<RegisterDto>(userForRegisterDto);

            await RegisterIdentityUser(registerDto, UserRoles.User);
            await _usersService.AddUser(userForRegisterDto);
        }

        private async Task RegisterIdentityUser(RegisterDto registerDto, string role)
        {
            if (!await CheckUsernameAvailability(registerDto.Username))
                throw new RegisterExistingUserException();

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                throw new InvalidCredentialsException();

            await AddRoleToUser(user, role);
        }

        public async Task Register(RegisterModel userForRegisterDto, string role)
        {
            if (await CheckUsernameAvailability(userForRegisterDto.Username))
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

        public async Task<bool> CheckUsernameAvailability(string username) =>
            await _userManager.FindByNameAsync(username) == null;

        private async Task AddRoleToUser(IdentityUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            if (await _roleManager.RoleExistsAsync(role))
                await _userManager.AddToRoleAsync(user, role);
        }
    }
}
