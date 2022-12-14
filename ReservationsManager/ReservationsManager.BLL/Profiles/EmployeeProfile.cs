using AutoMapper;
using ReservationsManager.Common.Dtos.Auth;
using ReservationsManager.Common.Dtos.Employees;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.BLL.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeForRegisterDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
