using AutoMapper;
using ReservationsManager.Common.Dtos.Users;
using ReservationsManager.Domain;

namespace ReservationsManager.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.UserName));
        }
    }
}
