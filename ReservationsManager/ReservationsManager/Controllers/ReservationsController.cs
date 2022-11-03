using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Dtos.Reservations;
using ReservationsManager.Domain;

namespace ReservationsManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;

        public ReservationsController(IReservationsService reservationsService) =>
            _reservationsService = reservationsService;

        [HttpGet]
        public async Task<IEnumerable<Reservation>> GetAll() =>
            await _reservationsService.GetAllAsync();

        [HttpGet("AvailableTimeBlocks")]
        public async Task<IActionResult> GetAvailableTimeBlocks([FromQuery] AvailableTimeBlocksRequestDto requestDto)
        {
            var availableTimeBlocks = await _reservationsService.GetAvailableTimeBlocksAsync(requestDto);
            return Ok(availableTimeBlocks);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] ReservationToAddDto reservationToAddDto)
        {
            await _reservationsService.AddReservation(reservationToAddDto);
            return Ok();
        }
    }
}
