using Services.Dtos.User;

namespace Services.Contracts
{
    public interface IUserService
    {
        public Task<UserDto> CreateUser(CreateUserDto createUserData);

        public Task<UserDto> LoginUser(LoginUserDto loginUserData);

        public IQueryable<UserDto> GetModerators();

        public Task<UserDto> ChangeLoginForModerator(string login, int id);

        public Task<UserDto> GenerateNewPasswordForModerator(int id);

        public Task DeleteModerator(int id);
    }
}