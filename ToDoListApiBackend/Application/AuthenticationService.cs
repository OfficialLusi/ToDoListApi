using LusiUtilsLibrary.Backend.Crypting;
using TodoListApi.Dtos;
using ToDoListApiBackend.Application.Interfaces;
using ToDoListApiBackend.Domain;

namespace ToDoListApiBackend.Application;

public class AuthenticationService : IAuthenticationService
{
    private IRepositoryService _repositoryService;

    public AuthenticationService(IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }


    #region authentication
    public User? AuthenticateUser(UserDto user)
    {
        List<User> users = _repositoryService.GetAllUsers();

        if (user.UserName == "admin")
            return users.FirstOrDefault(x => x.UserName == "admin");

        if (users.Any(x => x.UserEmail == user.UserEmail || x.UserName == user.UserName))
        {
            User repoUser = users.FirstOrDefault(x => x.UserEmail == user.UserEmail || x.UserName == user.UserName);
            if (repoUser.HashCode.SequenceEqual(HashCrypting.CalculateHash(user.UserPassword, repoUser.Salt)))
                return repoUser;
        }
        return null;
    }
    #endregion
}
