using AutoMapper;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Dtos.ActionEmployees;
using ReservationsManager.DAL.Interfaces;

namespace ReservationsManager.BLL.Services
{
    public class ActionEmployeesService: IActionEmployeesService
    {
        private readonly IActionEmployeesRepository _repository;
        private readonly IMapper _mapper;

        public ActionEmployeesService(IActionEmployeesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActionEmployeeDto>> GetAllByEmployeeIDAsync(int employeeID)
        {
            var actionEmployees = await _repository.GetAllByEmployeeIdAsync(employeeID);
            var dtos = _mapper.Map<IEnumerable<ActionEmployeeDto>>(actionEmployees);
            return dtos;
        }
    }
}
