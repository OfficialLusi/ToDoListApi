using LusiUtilsLibrary.Backend.Crypting;
using TodoListApi.Dtos;
using ToDoListApiBackend.Application.Interfaces;
using ToDoListApiBackend.Domain;

namespace ToDoListApiBackend.Application;

public class ApplicationService : IApplicationService
{
    private readonly IRepositoryService _repositoryService;
    private readonly ILogger<ApplicationService> _logger;

    public ApplicationService(IRepositoryService repositoryService, ILogger<ApplicationService> logger)
    {
        _repositoryService = repositoryService;
        _logger = logger;
    }

    public void AddUser(UserDto userDto)
    {
        byte[] salt = HashCrypting.GenerateSalt();
        byte[] hashCode = HashCrypting.CalculateHash(userDto.UserPassword, salt);

        User user = DtoFactory.GetUser(userDto, salt, hashCode);
        _repositoryService.AddUser(user);
    }

    public void UpdateUser(UserDto userDto)
    {
        User oldUser = _repositoryService.GetUser(UserProperties.UserGuid, userDto.UserGuid);

        if (oldUser == null) 
        {
            _logger.LogError("ApplicationService - UpdateUser user to update not found for userId <{userId}>", userDto.UserId);
            return;
        }

        User newUser;
        
        newUser = DtoFactory.GetUser(userDto, oldUser.Salt, oldUser.HashCode);
        
        _repositoryService.UpdateUser(newUser);
    }

    public void UpdateUserPassword(UserDto userDto)
    {
        User oldUser = _repositoryService.GetUser(UserProperties.UserGuid, userDto.UserGuid);

        if (oldUser == null)
        {
            _logger.LogError("ApplicationService - UpdateUser user to update not found for userId <{userId}>", userDto.UserId);
            return;
        }

        User newUser;

        byte[] newSalt = HashCrypting.GenerateSalt();
        byte[] newHashCode = HashCrypting.CalculateHash(userDto.UserPassword, newSalt);

        newUser = DtoFactory.GetUser(userDto, newSalt, newHashCode);

        _repositoryService.UpdateUser(newUser);
    }

    public void DeleteUser(UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public List<UserDto> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public UserDto GetUserByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }
}
