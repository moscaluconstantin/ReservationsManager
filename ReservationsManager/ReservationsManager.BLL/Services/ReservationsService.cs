using AutoMapper;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Dtos.Reservations;
using ReservationsManager.Common.Dtos.TimeBlocks;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;

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

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var reservations = await _reservationsRepository.GetAllOrderedByDateAsync();
            var reservationDtos = ExtractReservationDtos(reservations);
            return reservationDtos;
        }

        public async Task<IEnumerable<UserReservationDto>> GetAllByUserIdAsync(int userId)
        {
            var reservations = await _reservationsRepository.GetAllByUserIdAsync(userId);
            var rawReservationDtos = ExtractRawReservationDtos(reservations.ToList());
            return _mapper.Map<IEnumerable<UserReservationDto>>(rawReservationDtos);
        }

        public async Task<IEnumerable<TimeBlockDto>> GetAvailableTimeBlocksAsync(AvailableTimeBlocksRequestDto requestDto)
        {
            var actionEmployee = await _actionEmployeesRepository.GetByIdAsync(requestDto.ActionEmployeeId);
            var freeTimeBlocks = await GetFreeTimeBlocks(actionEmployee.EmployeeID, requestDto.Date);
            var availableTimeBlocks = GetAvailableTimeBlocks(freeTimeBlocks, actionEmployee.Duration);

            return _mapper.Map<IEnumerable<TimeBlockDto>>(availableTimeBlocks);
        }

        public async Task AddReservation(ReservationToAddDto reservationToAddDto)
        {
            var actionEmployee = await _actionEmployeesRepository.GetByIdAsync(reservationToAddDto.ActionEmployeeId);
            var timeBlocks = await _timeBlocksRepository.GetAllAsync();
            int duration = actionEmployee.Duration;

            Reservation[] reservations = CreateReservationsByDto(reservationToAddDto, timeBlocks, duration);

            await _reservationsRepository.AddRangeAsync(reservations);
            await _reservationsRepository.SaveAsync();
        }

        private IEnumerable<ReservationDto> ExtractReservationDtos(IEnumerable<Reservation> reservations)
        {
            var reservationDtos = new List<ReservationDto>();
            var groupedByDateReservations = reservations.OrderBy(x => x.TimeBlockID).GroupBy(x => x.Date.Date);

            foreach (var dateGroup in groupedByDateReservations)
            {
                var groupedByUserReservations = dateGroup.GroupBy(x => x.UserID);

                foreach (var userGroup in groupedByUserReservations)
                {
                    var userReservations = ExtractClientReservationDtos(userGroup);
                    reservationDtos.AddRange(userReservations);
                }
            }

            return reservationDtos;
        }

        private ReservationDto[] ExtractClientReservationDtos(IGrouping<int, Reservation> userGroup)
        {
            //var userReservations = userGroup.ToArray();
            //var ReservationDtos = new List<ReservationDto>();
            //int startIndex = 0;

            //for (int i = 0; i < userReservations.Length; i++)
            //{
            //    if (i != startIndex)
            //        continue;

            //    int endIndex = startIndex + userReservations[startIndex].ActionEmployee.Duration - 1;
            //    var reservationDto = _mapper.Map<ReservationDto>(userReservations[startIndex]);
            //    reservationDto.EndTime = userReservations[endIndex].TimeBlock.EndTime;
            //    ReservationDtos.Add(reservationDto);

            //    startIndex = endIndex + 1;
            //}

            //return ReservationDtos.ToArray();

            var rawReservations = ExtractRawReservationDtos(userGroup.ToList());
            return _mapper.Map<ReservationDto[]>(rawReservations);
        }

        private RawReservationDto[] ExtractRawReservationDtos(List<Reservation> reservations)
        {
            var reservationDtos = new List<RawReservationDto>();
            int startIndex = 0;

            for (int i = 0; i < reservations.Count; i++)
            {
                if (i != startIndex)
                    continue;

                int endIndex = startIndex + reservations[startIndex].ActionEmployee.Duration - 1;
                var reservationDto = _mapper.Map<RawReservationDto>(reservations[startIndex]);
                reservationDto.EndTimeBlock = reservations[endIndex].TimeBlock;
                reservationDtos.Add(reservationDto);

                startIndex = endIndex + 1;
            }

            return reservationDtos.ToArray();
        }

        private Reservation[] CreateReservationsByDto(ReservationToAddDto reservationToAddDto, IEnumerable<TimeBlock> timeBlocks, int duration)
        {
            var reservations = new Reservation[duration];
            for (int i = 0; i < reservations.Length; i++)
            {
                reservations[i] = _mapper.Map<Reservation>(reservationToAddDto);
                reservations[i].TimeBlock = timeBlocks.FirstOrDefault(x => x.Id == reservationToAddDto.TimeBlockId + i);
            }

            return reservations;
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
