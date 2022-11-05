using AutoMapper;
using ReservationsManager.Common.Dtos.TimeBlocks;
using ReservationsManager.Domain.Models;

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
