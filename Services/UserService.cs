using Domain;
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
        public async Task<UserDto> ChangeLoginForModerator(string login, int id)
        {
            var user = await GetUserAndCheckRole(id);

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

        public async Task DeleteModerator(int id)
        {
            await GetUserAndCheckRole(id);

            await _dbRepository.Delete<User>(id);

            await _dbRepository.Context.SaveChangesAsync();
        }

        public async Task<UserDto> GenerateNewPasswordForModerator(int id)
        {
            var user = await GetUserAndCheckRole(id);

            user.Password = _passwordHasher.HashPassword(Path.GetRandomFileName());

            return await base.Edit(user, true);
        }

        async Task<User> GetUserAndCheckRole(int id)
        {
            var user = await _dbRepository.GetByIdAsync<User>(id);

            if (user.Role != UserRole.MODERATOR)
                throw new PermissionDenied($"user with {id} isn't moderator");

            return user;
        }

        public IQueryable<UserDto> GetModerators() => base._dbRepository
            .GetItems<User>()
            .Where(u => u.Role == UserRole.MODERATOR)
            .ProjectToType<UserDto>();



    }

}