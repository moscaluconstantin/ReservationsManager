using AutoMapper;
using ReservationsManager.Common.Dtos.Auth;
using ReservationsManager.Common.Dtos.Users;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.UserName));
        }
    }
}
