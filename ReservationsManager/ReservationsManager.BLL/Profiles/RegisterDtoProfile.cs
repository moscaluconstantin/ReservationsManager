using AutoMapper;
using ReservationsManager.Common.Dtos.Auth;

namespace ReservationsManager.BLL.Profiles
{
    public class RegisterDtoProfile : Profile
    {
        public RegisterDtoProfile()
        {
            CreateMap<UserForRegisterDto, RegisterDto>();
        }
    }
}
