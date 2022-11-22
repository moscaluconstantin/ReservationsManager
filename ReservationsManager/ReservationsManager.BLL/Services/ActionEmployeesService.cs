using AutoMapper;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Dtos.ActionEmployees;
using ReservationsManager.Common.Dtos.Employees;
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
            return _mapper.Map<IEnumerable<ActionEmployeeDto>>(actionEmployees);
        }

        public async Task<IEnumerable<WorkingEmployeeDto>> GetWorkingEmployeesAsync()
        {
            var employees = await _repository.GetEmployeesAsync();
            return _mapper.Map<IEnumerable<WorkingEmployeeDto>>(employees);
        }
    }
}
