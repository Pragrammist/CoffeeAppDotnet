using Domain.Exceptions;
using EFCore;
using EFCore.Models;
using Mapster;
using Services.Common;
using Services.Contracts;
using Services.Dtos.User;


namespace Services
{
    public class UserService : DbServiceBase<UserDto, User>, IUserService
    {
        readonly IHasherService _passwordHasher;
        readonly IAuthService _authService;
        public UserService(IRepository repository, IHasherService passwordHasher, IAuthService authService) : base(repository)
        {
            _passwordHasher = passwordHasher;
            _authService = authService;
        }
        public async Task<UserDto> LoginUser(LoginUserDto loginUserData)
        {
            loginUserData.Password = _passwordHasher.HashPassword(loginUserData.Password);

            var user = _dbRepository.GetItems<User>().FirstOrDefault(f => 
                (
                    f.Email == loginUserData.LoginOrEmail || 
                    f.Login == loginUserData.LoginOrEmail
                ) && f.Password == loginUserData.Password);

            if (user is null)
                throw new UserDataNotValidException("user not found");

            return await Task.FromResult(user.Adapt<UserDto>());
        }
        public async Task<UserDto> ChangeLogin(string login, int id)
        {
            var user = await _dbRepository.GetByIdAsync<User>(id) ?? throw new NotFoundException(id);

            user.Login = login;

            return await base.Edit(user, true);
        }

        public async Task<UserDto> CreateUser(CreateUserDto createUserData)
        {
            createUserData.Password = _passwordHasher.HashPassword(createUserData.Password);

            var extUser = _dbRepository.GetItems<User>().FirstOrDefault(f => f.Email == createUserData.Email || f.Login == createUserData.Login);

            if(extUser is not null)
                throw new UserDataNotValidException("user already exists");

            var newUserId = await base.Create(createUserData.Adapt<User>());

            return newUserId;
        }

        public async Task DeleteUser(int id)
        {
            await _dbRepository.Delete(id);

            await _dbRepository.Context.SaveChangesAsync();
        }

        public async Task<UserDto> GenerateNewPassword(int id)
        {
            var user = await _dbRepository.GetByIdAsync<User>(id);

            user.Password = _passwordHasher.HashPassword(Path.GetRandomFileName());

            return await base.Edit(user, true);
        }

        public IEnumerable<UserDto> GetUsers() => base._dbRepository.GetItems<User>().ProjectToType<UserDto>();



    }

}