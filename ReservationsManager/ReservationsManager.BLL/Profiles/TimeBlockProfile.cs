using AutoMapper;
using ReservationsManager.Common.Dtos.TimeBlocks;
using ReservationsManager.Domain;

namespace ReservationsManager.BLL.Profiles
{
    public class TimeBlockProfile : Profile
    {
        public TimeBlockProfile()
        {
            CreateMap<TimeBlock,TimeBlockDto>();
        }
    }
}
