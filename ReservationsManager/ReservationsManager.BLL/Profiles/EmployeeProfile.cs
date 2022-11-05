using AutoMapper;
using ReservationsManager.Common.Dtos.Employees;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.BLL.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.UserName));
        }
    }
}
