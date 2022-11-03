using AutoMapper;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Dtos.Employees;
using ReservationsManager.DAL.Interfaces;

namespace ReservationsManager.BLL.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _repository;
        private readonly IMapper _mapper;

        public EmployeesService(IEmployeesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeeDtos;
        }
    }
}
