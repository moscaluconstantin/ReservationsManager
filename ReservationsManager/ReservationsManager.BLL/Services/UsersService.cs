using AutoMapper;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common.Dtos.Auth;
using ReservationsManager.Common.Dtos.Users;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }

        public async Task<IEnumerable<User>> GetAllNativeAsync() =>
            await _repository.GetAllAsync();

        public async Task AddUserAsync(UserForRegisterDto userForRegisterDto)
        {
            var user = _mapper.Map<User>(userForRegisterDto);
            await _repository.AddAsync(user);
            await _repository.SaveAsync();
        }

        public async Task<int> GetIdByUernameAsync(string username)
        {
            var user = await _repository.GetByUernameAsync(username);
            return user != null ? user.Id : -1;
        }
    }
}
