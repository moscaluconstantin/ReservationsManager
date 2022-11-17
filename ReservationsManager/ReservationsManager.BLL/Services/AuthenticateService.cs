using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common;
using ReservationsManager.Common.Dtos.Auth;
using ReservationsManager.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ReservationsManager.BLL.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUsersService _usersService;
        private readonly IEmployeesService _employeesService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private Dictionary<string, IGetIdService> _getIdServices;

        public AuthenticateService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUsersService usersService,
            IEmployeesService employeesService,
            IJwtTokenService jwtTokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _usersService = usersService;
            _employeesService = employeesService;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;

            _getIdServices = new Dictionary<string, IGetIdService>()
            {
                [UserRoles.Admin] = _employeesService,
                [UserRoles.Employee] = _employeesService,
                [UserRoles.User] = _usersService,
            };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginModel userForLoginDto)
        {
            var jwtToken = await LoginIdentityUserAsync(userForLoginDto);

            if (string.IsNullOrEmpty(jwtToken))
                return null;

            var role = await GetRoleByUsernameAsync(userForLoginDto.Username);

            if (string.IsNullOrEmpty(role))
                return null;

            int id = await GetIdAsync(userForLoginDto.Username, role);

            if (id == -1)
                return null;

            return new LoginResponseDto()
            {
                AccessToken = jwtToken,
                Role = role,
                Id = id
            };
        }

        public async Task RegisterEmployeeAsync(EmployeeForRegisterDto employeeForRegisterDto)
        {
            var registerDto = _mapper.Map<RegisterDto>(employeeForRegisterDto);

            await RegisterIdentityUserAsync(registerDto, UserRoles.Employee);
            await _employeesService.AddEmployeeAsync(employeeForRegisterDto);
        }

        public async Task RegisterUserAsync(UserForRegisterDto userForRegisterDto)
        {
            var registerDto = _mapper.Map<RegisterDto>(userForRegisterDto);

            await RegisterIdentityUserAsync(registerDto, UserRoles.User);
            await _usersService.AddUserAsync(userForRegisterDto);
        }

        public async Task<bool> CheckUsernameAvailabilityAsync(string username) =>
            await _userManager.FindByNameAsync(username) == null;

        private async Task<string> LoginIdentityUserAsync(LoginModel userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, userForLoginDto.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = userRoles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

                return _jwtTokenService.Generate(user.Id, authClaims);
            }

            return string.Empty;
        }

        private async Task RegisterIdentityUserAsync(RegisterDto registerDto, string role)
        {
            if (!await CheckUsernameAvailabilityAsync(registerDto.Username))
                throw new RegisterExistingUserException();

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                throw new InvalidCredentialsException();

            await AddRoleToUserAsync(user, role);
        }

        private async Task AddRoleToUserAsync(IdentityUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            if (await _roleManager.RoleExistsAsync(role))
                await _userManager.AddToRoleAsync(user, role);
        }

        private async Task<string> GetRoleByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return string.Empty;

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count == 0)
                return string.Empty;

            return userRoles[0];
        }

        private async Task<int> GetIdAsync(string username, string role)
        {
            if (!_getIdServices.TryGetValue(role, out var service))
                return -1;

            return await service.GetIdByUernameAsync(username);
        }
    }
}
