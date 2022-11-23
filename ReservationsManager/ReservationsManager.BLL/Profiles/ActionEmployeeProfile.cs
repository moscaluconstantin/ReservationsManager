using AutoMapper;
using ReservationsManager.Common.Dtos.ActionEmployees;
using ReservationsManager.Common.Dtos.Employees;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.BLL.Profiles
{
    public class ActionEmployeeProfile : Profile
    {
        public ActionEmployeeProfile()
        {
            CreateMap<ActionEmployee, ActionEmployeeDto>()
                .ForMember(x => x.ActionName, y => y.MapFrom(z => z.Action.Name));

            CreateMap<ActionEmployee, WorkingEmployeeDto>()
                .ForMember(x => x.ActionEmployeeId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.EmployeeName, y => y.MapFrom(z => z.Employee.Name));
        }
    }
}
