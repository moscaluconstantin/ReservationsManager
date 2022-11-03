using AutoMapper;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Dtos.Reservations;
using ReservationsManager.Common.Dtos.TimeBlocks;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain;
using System.Collections;

namespace ReservationsManager.BLL.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IActionEmployeesRepository _actionEmployeesRepository;
        private readonly ITimeBlocksRepository _timeBlocksRepository;
        private readonly IMapper _mapper;

        public ReservationsService(
            IReservationsRepository reservationsRepository,
            IActionEmployeesRepository actionEmployeesRepository,
            ITimeBlocksRepository timeBlocksRepository,
            IMapper mapper)
        {
            _reservationsRepository = reservationsRepository;
            _actionEmployeesRepository = actionEmployeesRepository;
            this._timeBlocksRepository = timeBlocksRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync() =>
            await _reservationsRepository.GetAllAsync();

        public async Task<IEnumerable<TimeBlockDto>> GetAvailableTimeBlocksAsync(AvailableTimeBlocksRequestDto requestDto)
        {
            var actionEmployee = await _actionEmployeesRepository.GetByIdAsync(requestDto.ActionEmployeeId);
            var freeTimeBlocks = await GetFreeTimeBlocks(actionEmployee.EmployeeID, requestDto.Date);
            var availableTimeBlocks = GetAvailableTimeBlocks(freeTimeBlocks, actionEmployee.Duration);

            return _mapper.Map<IEnumerable<TimeBlockDto>>(availableTimeBlocks);
        }

        public async Task AddReservation(ReservationToAddDto reservationToAddDto)
        {
            var baseReservation = _mapper.Map<Reservation>(reservationToAddDto);
            var actionEmployee = await _actionEmployeesRepository.GetByIdAsync(baseReservation.ActionEmployeeID);
            int duration = actionEmployee.Duration;

            for (int i = 0; i < duration; i++)
            {
                await _reservationsRepository.AddAsync(baseReservation);
                baseReservation.TimeBlockID++;
            }

            await _reservationsRepository.SaveAsync();
        }

        private async Task<List<TimeBlock>> GetFreeTimeBlocks(int employeeId, DateTime date)
        {
            var reservedTimeBlocks = await _reservationsRepository.GetReservedTimeBlockByEmployeeIdAsync(employeeId, date);
            var timeBlocks = await _timeBlocksRepository.GetAllAsync();

            return timeBlocks.Where(x => !reservedTimeBlocks.Contains(x)).OrderBy(x => x.Id).ToList();
        }

        private IEnumerable<TimeBlock> GetAvailableTimeBlocks(List<TimeBlock> freeTimeBlocks, int duration)
        {
            var availableTimeBlocks = new List<TimeBlock>();

            if (freeTimeBlocks.Count < duration)
                return availableTimeBlocks;

            for (int i = 0; i <= freeTimeBlocks.Count - duration; i++)
            {
                bool isValid = true;

                for (int j = 0; j < duration - 1; j++)
                {
                    if (freeTimeBlocks[i + j + 1].Id - freeTimeBlocks[i + j].Id > 1)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    availableTimeBlocks.Add(freeTimeBlocks[i]);
                }
            }

            return availableTimeBlocks;
        }
    }
}
