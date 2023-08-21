using Domain.Enums;
using Domain.Exceptions;
using Domain.Helpers.Hashing;
using EFCore;
using EFCore.Models;
using Mapster;
using Services.Common;
using Services.Contracts;
using Services.Dtos.User;
using System.Text;

namespace Services
{
    public class UserService : DbServiceBase<UserDto, User>, IUserService
    {
        
        public UserService(IRepository repository) : base(repository)
        {
            
        }
        public async Task<UserDto> LoginUser(LoginUserDto loginUserData)
        {
            loginUserData.Password = loginUserData.Password.Hash();

            var user = _dbRepository.Context.Users.FirstOrDefault(f => 
                (
                    f.Email == loginUserData.LoginOrEmail || 
                    f.Login == loginUserData.LoginOrEmail
                ) && f.Password == loginUserData.Password);

            if (user is null)
                throw new UserDataNotValidException("user not found", 404);

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
            createUserData.Password = createUserData.Password.Hash();

            var extUser = _dbRepository.Context.Users.FirstOrDefault(f => f.Email == createUserData.Email || f.Login == createUserData.Login);

            if(extUser is not null)
                throw new UserDataNotValidException("user already exists", 401);

            var newUserId = await base.Create(createUserData);

            return newUserId;
        }

        public async Task DeleteModerator(int id)
        {
            await GetUserAndCheckRole(id);

            await base.Delete(id);
        }

        public async Task<string> GenerateNewPasswordForModerator(int id)
        {
            var user = await GetUserAndCheckRole(id);

            var newPassword = NewRandomPassword();
            user.Password = newPassword.Hash();

            await base.Edit(user, true);

            return newPassword;
        }
        string NewRandomPassword()
        {
            var newPassword = Path.GetRandomFileName().Replace(".",string.Empty);

            var newPasswordBytes = Encoding.UTF8.GetBytes(newPassword);

            var base64Password = Convert.ToBase64String(newPasswordBytes).Replace("=", string.Empty);

            return base64Password;
        }
            

        async Task<User> GetUserAndCheckRole(int id)
        {
            var user = await base.GetByIdEntityAsync(id);

            if (user.Role != UserRole.MODERATOR)
                throw new PermissionDenied($"user with {id} isn't moderator", 403);

            return user;
        }

        public IQueryable<UserDto> GetModerators() => 
            _dbRepository.Context.Users
            .Where(u => u.Role == UserRole.MODERATOR)
            .ProjectToType<UserDto>();
            
            



    }

}