using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Dtos.Auth;
using ReservationsManager.DAL.Interfaces;

namespace ReservationsManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserCredentialsRepository _repository;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginController(IUserCredentialsRepository repository, IJwtTokenService jwtTokenService)
        {
            _repository = repository;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            var credentials = await _repository.GetByLoginAsync(requestDto.Username);

            if (credentials == null || credentials.Password != requestDto.Password)
                return Unauthorized("Wrong login or password.");

            var token = _jwtTokenService.Generate(credentials.Id, "User");

            return Ok(new { AccessToken = token });
        }
    }
}
