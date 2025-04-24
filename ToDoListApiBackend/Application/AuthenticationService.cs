using LusiUtilsLibrary.Backend.Crypting;
using TodoListApi.Dtos;
using ToDoListApiBackend.Application.Interfaces;

namespace ToDoListApiBackend.Application;

public class AuthenticationService : IAuthenticationService
{
    private IRepositoryService _repositoryService;


    #region authentication
    public UserDto? AuthenticateUser(UserDto user)
    {
        List<UserDto> users = _repositoryService.GetAllUsers();

        if (user.UserName == "admin")
            return users.FirstOrDefault(x => x.UserName == "admin");

        if (users.Any(x => x.UserEmail == user.UserEmail || x.UserName == user.UserName))
        {
            UserDto repoUser = users.FirstOrDefault(x => x.UserEmail == user.UserEmail || x.UserName == user.UserName);
            if (Convert.FromBase64String(repoUser.HashCode).SequenceEqual(HashCrypting.CalculateHash(user.UserPassword, Convert.FromBase64String(repoUser.Salt))))
                return repoUser;
        }
        return null;
    }
    #endregion
}
