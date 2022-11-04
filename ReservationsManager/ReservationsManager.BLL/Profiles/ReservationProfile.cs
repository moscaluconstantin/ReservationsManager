using AutoMapper;
using ReservationsManager.Common.Dtos.Reservations;
using ReservationsManager.Domain;

namespace ReservationsManager.BLL.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationToAddDto, Reservation>();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(x => x.ClientName, y => y.MapFrom(z => z.User.UserName))
                .ForMember(x => x.EmployeeName, y => y.MapFrom(z => z.ActionEmployee.Employee.UserName))
                .ForMember(x => x.StartTime, y => y.MapFrom(z => z.TimeBlock.StartTime));
        }
    }
}
