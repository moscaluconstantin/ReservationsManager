using AutoMapper;
using ReservationsManager.Common.Dtos.ActionEmployees;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.BLL.Profiles
{
    public class ActionEmployeeProfile: Profile
    {
        public ActionEmployeeProfile()
        {
            CreateMap<ActionEmployee, ActionEmployeeDto>()
                .ForMember(x => x.ActionName, y => y.MapFrom(z => z.Action.Name));
        }
    }
}
