using AutoMapper;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Dtos.Auth;
using ReservationsManager.Common.Dtos.Employees;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;

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

        public async Task AddEmployeeAsync(EmployeeForRegisterDto employeeForRegisterDto)
        {
            var employee = _mapper.Map<Employee>(employeeForRegisterDto);
            employee.ExperienceStartDate = DateTime.Now - TimeSpan.FromDays(employeeForRegisterDto.Experience * 30);

            await _repository.AddAsync(employee);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeeDtos;
        }

        public async Task<int> GetIdByUernameAsync(string username)
        {
            var employee = await _repository.GetByUsernameAsync(username);
            return employee != null ? employee.Id : -1;
        }
    }
}
