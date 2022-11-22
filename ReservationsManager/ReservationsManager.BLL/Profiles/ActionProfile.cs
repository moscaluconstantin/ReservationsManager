using AutoMapper;
using ReservationsManager.Common.Dtos.Actions;
using Action = ReservationsManager.Domain.Models.Action;

namespace ReservationsManager.BLL.Profiles
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<Action, AssignedActionDto>();
                //.ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                //.ForMember(x => x.Name, y => y.MapFrom(z => z.Name));
        }
    }
}
