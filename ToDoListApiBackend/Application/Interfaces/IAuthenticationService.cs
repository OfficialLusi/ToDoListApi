using TodoListApi.Dtos;

namespace ToDoListApiBackend.Application.Interfaces;

public interface IAuthenticationService
{
    public UserDto AuthenticateUser(UserDto userDto);
}
