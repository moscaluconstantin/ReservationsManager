using AutoMapper;
using ReservationsManager.Common.Dtos.Reservations;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.BLL.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationToAddDto, Reservation>();

            CreateMap<Reservation, RawReservationDto>()
                .ForMember(x => x.Employee, y => y.MapFrom(z => z.ActionEmployee.Employee))
                .ForMember(x => x.Action, y => y.MapFrom(z => z.ActionEmployee.Action))
                .ForMember(x => x.StartTimeBlock, y => y.MapFrom(z => z.TimeBlock));

            CreateMap<RawReservationDto, ReservationDto>()
                .ForMember(x => x.ClientName, y => y.MapFrom(z => z.User.Name))
                .ForMember(x => x.EmployeeName, y => y.MapFrom(z => z.Employee.Name))
                .ForMember(x => x.StartTime, y => y.MapFrom(z => z.StartTimeBlock.StartTime))
                .ForMember(x => x.EndTime, y => y.MapFrom(z => z.EndTimeBlock.EndTime));

            CreateMap<RawReservationDto, UserReservationDto>()
                .ForMember(x => x.EmployeeName, y => y.MapFrom(z => z.Employee.Name))
                .ForMember(x => x.ActionName, y => y.MapFrom(z => z.Action.Name))
                .ForMember(x => x.StartTime, y => y.MapFrom(z => z.StartTimeBlock.StartTime));
        }
    }
}
