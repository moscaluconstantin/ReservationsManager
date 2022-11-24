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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("All")]
        public async Task<IEnumerable<ReservationDto>> GetAll() =>
            await _reservationsService.GetAllAsync();

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("AvailableTimeBlocks")]
        public async Task<IActionResult> GetAvailableTimeBlocks([FromQuery] AvailableTimeBlocksRequestDto requestDto)
        {
            var availableTimeBlocks = await _reservationsService.GetAvailableTimeBlocksAsync(requestDto);
            return Ok(availableTimeBlocks);
        }

        [Authorize(Roles = UserRoles.Employee)]
        [HttpGet("ForEmployee/{employeeId}")]
        public async Task<IActionResult> GetEmployeeReservations(int employeeId)
        {
            var reservations = await _reservationsService.GetAllByEmployeeIdAsync(employeeId);
            return Ok(reservations);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ForUser/{userId}")]
        public async Task<IActionResult> GetUserReservations(int userId)
        {
            var reservations = await _reservationsService.GetAllByUserIdAsync(userId);
            return Ok(reservations);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] ReservationToAddDto reservationToAddDto)
        {
            await _reservationsService.AddReservation(reservationToAddDto);
            return Ok();
        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateCanceledState([FromBody] ReservationCanceledUpdateDto updateDto)
        {
            if (updateDto.Id < 0)
            {
                return BadRequest("Invalid reservation id");
            }

            await _reservationsService.UpdateReservation(updateDto);
            return Ok();
        }
    }
}
