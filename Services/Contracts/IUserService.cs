using Services.Dtos.User;

namespace Services.Contracts
{
    public interface IUserService
    {
        public Task<UserDto> CreateUser(CreateUserDto createUserData);

        public Task<UserDto> LoginUser(LoginUserDto loginUserData);

        public IEnumerable<UserDto> GetUsers();

        public Task<UserDto> ChangeLogin(string login, int id);

        public Task<UserDto> GenerateNewPassword(int id);

        public Task DeleteUser(int id);
    }
}