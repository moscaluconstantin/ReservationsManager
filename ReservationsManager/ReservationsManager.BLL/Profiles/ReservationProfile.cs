using AutoMapper;
using ReservationsManager.Common.Dtos.Reservations;
using ReservationsManager.Domain;

namespace ReservationsManager.BLL.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationToAddDto, Reservation>()
                .ForMember(x => x.TimeBlockID, y => y.MapFrom(z => z.StartTimeBlockId));
        }
    }
}
