using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common;
using ReservationsManager.Common.Dtos.Reservations;

namespace ReservationsManager.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;

        public ReservationsController(IReservationsService reservationsService) =>
            _reservationsService = reservationsService;

        [HttpGet("All")]
        public async Task<IEnumerable<ReservationDto>> GetAll() =>
            await _reservationsService.GetAllAsync();

        [HttpGet("AvailableTimeBlocks")]
        public async Task<IActionResult> GetAvailableTimeBlocks([FromQuery] AvailableTimeBlocksRequestDto requestDto)
        {
            var availableTimeBlocks = await _reservationsService.GetAvailableTimeBlocksAsync(requestDto);
            return Ok(availableTimeBlocks);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ForUser/{userId}")]
        public async Task<IActionResult> GetUserReservations(int userId)
        {
            var reservations = await _reservationsService.GetAllByUserIdAsync(userId);
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] ReservationToAddDto reservationToAddDto)
        {
            await _reservationsService.AddReservation(reservationToAddDto);
            return Ok();
        }
    }
}
